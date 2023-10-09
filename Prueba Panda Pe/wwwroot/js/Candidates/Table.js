$(document).ready(function () {
    localStorage.removeItem("candidate");
    cargarDatos();
});

const tablaCandidatos = $("#tabla-candidatos");
const formularioExperiencia = $("#formAgregarExperiencia");
const modalAgregarExperiencia = $("#modalAgregarExperiencia");
const btnAgregarExperiencia = $("#btn-agregar-experiencia");
const tablaExperiencias = $("#tablaExperiencias");
const modalExperiencia = $("#modalExperiencia");
const botonCrearEditarCandidato = $(".guardar-editar-candidato");
const modalCandidato = $("#modalCandidato");
const formCandidato = $("#formCandidato");


// Función para cargar los datos desde la API
function cargarDatos() {
    tablaCandidatos.empty();
    let arrayData = [];
    $.ajax({
        url: "../api/Candidate",
        method: "GET",
        dataType: "json",
        success: function (data) {
            data.forEach(function (item) {
                var nombresCompletos = `${item.name} ${item.surname}`;
                arrayData.push(item);
                var row = `<tr>
                    <td>${item.idCandidate}</td>
                    <td>${nombresCompletos}</td>
                    <td>${item.email}</td>
                    <td>${item.birthday}</td>
                    <td>${item.insertDate}</td>
                    <td>
                        <button class="btn btn-primary btn-editar guardar-editar-candidato" data-id="${item.idCandidate}">Editar</button>
                        <button class="btn btn-danger btn-eliminar eliminar-candidato" data-id="${item.idCandidate}">Eliminar</button>
                        <button class="btn btn-success btn-mostrar-experiencia" data-id="${item.idCandidate}">Experiencias</button>
                    </td>
                </tr>`;

                tablaCandidatos.append(row);
            });

            $(".guardar-editar-candidato").on("click", function () {

                if ($(this).data('id')) {

                        crearInputHidden("idCandidate", $(this).data('id'), formCandidato);

                        var experienciaSelect = obtenerExperienciaPorId($(this).data('id'), JSON.parse(localStorage.getItem("candidate")));
                        if (experienciaSelect) {
                            setTimeout(() => {
                                $("#idCandidate").val(experienciaSelect.idCandidate);
                                $("#name").val(experienciaSelect.name);
                                $("#surname").val(experienciaSelect.surname);
                                $("#birthday").val(experienciaSelect.birthday);
                                $("#email").val(experienciaSelect.email);
                            }, 500);
                        }
                    }

                    // Limpiar el formulario antes de mostrar el modal
                    formCandidato[0].reset();
                    modalCandidato.modal("show");
                
            })

            $(".eliminar-candidato").on("click", function () {
                const id = $(this).data('id');
                // Realizar la solicitud DELETE al API
                $.ajax({
                    url: `../api/Candidate/${id}`,
                    method: "DELETE",
                    dataType: "json",
                    success: function (data) {
                        cargarDatos();

                        Swal.fire({
                            html: `Se eliminó con éxito el candidato`,
                            icon: "success"
                        });
                    },
                    error: function () {
                        console.log("Error al eliminar el candidato.");
                    }
                });
            })

            localStorage.setItem("candidate", JSON.stringify(arrayData));

            // Cuando se hace clic en el botón "Experiencias"
            $(".btn-mostrar-experiencia").click(function () {
                localStorage.removeItem("idCandidate");
                imprimirTablaExperiencias($(this).data('id'));
            });
        },
        error: function (e) {
            console.error(e.responseText);
            Swal.fire({
                html: `Error al cargar los candidatos. <b>${e.responseText}</b>`,
                icon: "error"
            });
        }
    });
}

// Agregar experiencia
$(".btn-agregar-editar-experiencia").on("click", function () {
    var fila = $(this).closest("tr");

    if ($(this).data('idedit')) {
        crearInputHidden("idCandidateExperience", $(this).data('idedit'), formularioExperiencia);
    }

    // Limpiar el formulario antes de mostrar el modal
    formularioExperiencia[0].reset();

    // Mostrar el modal
    modalAgregarExperiencia.modal("show");
});

// Guardar experiencia
$("#btnGuardarExperiencia").on("click", function () {
    let url = "../api/CandidateExperience";
    // Obtiene los valores del formulario y crea un objeto JSON
    var experienciaData = {
        "IdCandidate": $("#idCandidate").val() ?? localStorage.getItem("idCandidate"),
        "Company": $("#company").val(),
        "Job": $("#job").val(),
        "Description": $("#description").val(),
        "Salary": parseFloat($("#salary").val()),
        "BeginDate": $("#beginDate").val(),
        "EndDate": $("#endDate").val()
    };

    // Verifica si el input idCandidateExperience existe en el formulario
    if ($("#idCandidateExperience").length) {
        experienciaData["Id"] = $("#idCandidateExperience").val();
        url = `${url}/${$("#idCandidateExperience").val()}`;
    }

    // Realizar la solicitud POST al API
    guardarExperiencia(url, experienciaData);
});

function crearInputHidden(name, value, formulario) {
    var inputHidden = $("<input>")
        .attr("type", "hidden")
        .attr("name", name)
        .attr("id", name)
        .val(value);

    // Agrega el input hidden al formulario
    formulario.append(inputHidden);
}

function obtenerNombreMes(numeroMes) {
    const meses = [
        "enero", "febrero", "marzo", "abril", "mayo", "junio",
        "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"
    ];
    return meses[numeroMes];
}

function imprimirTablaExperiencias(idCandidate = null) {
    var idCandidato = idCandidate;
    localStorage.setItem("idCandidate", idCandidate);

    $.ajax({
        url: `../api/CandidateExperience/experience/${idCandidato}`,
        method: "GET",
        success: function (data) {
            var arrayData = [];
            tablaExperiencias.empty();
            localStorage.removeItem("candidateInfo");
            data.forEach(function (experiencia) {
                arrayData.push(experiencia);
                // Formatear la fecha de inicio
                var fechaInicio = new Date(experiencia.beginDate);
                var fechaInicioFormateada = `${fechaInicio.getDate()} de ${obtenerNombreMes(fechaInicio.getMonth())} del ${fechaInicio.getFullYear()}`;

                // Formatear la fecha de fin o indicar "Actualmente trabajando" si endDate es nulo
                var fechaFinFormateada = "Actualmente trabajando";
                if (experiencia.endDate !== null) {
                    var fechaFin = new Date(experiencia.endDate);
                    fechaFinFormateada = `${fechaFin.getDate()} de ${obtenerNombreMes(fechaFin.getMonth())} del ${fechaFin.getFullYear()}`;
                }

                var fila = `<tr>
                    <td>${experiencia.company}</td>
                    <td>${experiencia.job}</td>
                    <td>${experiencia.description}</td>
                    <td>${experiencia.salary}</td>
                    <td>${fechaInicioFormateada}</td>
                    <td>${fechaFinFormateada}</td>
                    <td>
                        <button class="btn btn-primary btn-editar btn-agregar-editar-experiencia" data-edit="${experiencia.idCandidateExperience}">Editar</button>
                        <button class="btn btn-danger btn-eliminar btn-eliminar-experiencia" data-id="${experiencia.idCandidateExperience}">Eliminar</button>
                    </td>
                </tr>`;
                tablaExperiencias.append(fila);
                idCandidate = experiencia.idCandidate;                
            });
            localStorage.setItem("candidateInfo", JSON.stringify(arrayData));

            btnAgregarExperiencia.data("id", idCandidate);
            // Eliminar Experiencia
            $(".btn-eliminar-experiencia").on("click", function () {
                const idExperience = $(this).data('id');
                // Realizar la solicitud POST al API
                eliminarExperiencia(idExperience);
            })
            // Editar experiencia
            $(".btn-agregar-editar-experiencia").on("click", function () {
                var formulario = formularioExperiencia;
                var fila = $(this).closest("tr");
                var idCandidate = fila.find("td:eq(0)").text();
                $("#idCandidate").val(localStorage.getItem("idCandidate"));

                if (localStorage.getItem("candidateInfo") && $(this).data('edit')) {

                    // Crea un nuevo input hidden
                    var inputHidden = $("<input>")
                        .attr("type", "hidden")
                        .attr("name", "idCandidateExperience")
                        .attr("id", "idCandidateExperience")
                        .val($(this).data('idedit'));

                    // Agrega el input hidden al formulario
                    formulario.append(inputHidden);

                    var experienciaSelect = obtenerExperienciaPorId($(this).data('edit'), JSON.parse(localStorage.getItem("candidateInfo")), true);
                    if (experienciaSelect) {
                        llenarFormularioConExperiencia(experienciaSelect);
                    }
                }

                // Limpiar el formulario antes de mostrar el modal
                formulario[0].reset();

                // Mostrar el modal
                modalAgregarExperiencia.modal("show");
            });
            // Mostrar el modal
            modalExperiencia.modal("show");
        },
        error: function (e) {
            console.error(e.responseText);
            Swal.fire({
                html: `Error al cargar las experiencias del candidato. <b>${e.responseText}</b>`,
                icon: "error"
            });
        }
    });
}

function obtenerExperienciaPorId(id, dataSearch, isExperience = false) {
    for (let i = 0; i < dataSearch.length; i++) {
        let idSearch = (isExperience) ? dataSearch[i].idCandidateExperience : dataSearch[i].idCandidate;
        if (idSearch === id) {
            return dataSearch[i];
        }
    }
    return null;
}

function llenarFormularioConExperiencia(experiencia) {
    setTimeout(() => {
        $("#idCandidateExperience").val(experiencia.idCandidateExperience);
        $("#idCandidate").val(localStorage.getItem("idCandidate"));
        $("#company").val(experiencia.company);
        $("#job").val(experiencia.job);
        $("#description").val(experiencia.description);
        $("#salary").val(experiencia.salary);
        $("#beginDate").val(experiencia.beginDate.substring(0, 10));
        $("#endDate").val(experiencia.endDate ? experiencia.endDate.substring(0, 10) : "");
    }, 500);
}

function guardarExperiencia(url, experienciaData) {
    // Convierte el objeto JSON en una cadena JSON
    const experienciaJSON = JSON.stringify(experienciaData);

    // Realizar la solicitud POST al API
    $.ajax({
        url: url, // Cambia la URL a la ruta correcta de tu API
        method: ($("#idCandidateExperience").length) ? "PUT" : "POST",
        data: experienciaJSON,
        contentType: "application/json", // Especifica el tipo de contenido como JSON
        dataType: "json",
        success: function (data) {
            // Cerrar el modal después de guardar
            modalAgregarExperiencia.modal("hide");

            // Puedes actualizar la tabla o recargar los datos aquí si es necesario
            imprimirTablaExperiencias(localStorage.getItem("idCandidate"));
            Swal.fire({
                html: `Los cambios fueron realizados`,
                icon: "success"
            });
        },
        error: function (e) {
            console.error(e.responseText);
            Swal.fire({
                html: `Error al guardar la experiencia. <b>${e.responseText}</b>`,
                icon: "error"
            });
        }
    });
}

function eliminarExperiencia(id) {
    // Realizar la solicitud DELETE al API
    $.ajax({
        url: `../api/CandidateExperience/${id}`, // Cambia la URL a la ruta correcta de tu API
        method: "DELETE",
        dataType: "json",
        success: function (data) {
            // Puedes actualizar la tabla o recargar los datos aquí si es necesario
            imprimirTablaExperiencias(localStorage.getItem("idCandidate"));

            Swal.fire({
                html: `Se ha eliminado la experiencia con éxito`,
                icon: "success"
            });
        },
        error: function (e) {
            console.error(e.responseText);
            Swal.fire({
                html: `Error al eliminar la experiencia. <b>${e.responseText}</b>`,
                icon: "error"
            });        }
    });
}


$("#btnGuardarCambios").on("click", function () {
    let url = "../api/Candidate";
    // Obtén los valores del formulario y crea un objeto JSON
    var experienciaData = {
        "Name": $("#name").val(),
        "Surname": $("#surname").val(),
        "Birthday": $("#birthday").val(),
        "Email": $("#email").val(),
    };

    // Verificar si el input idCandidate existe en el formulario
    if ($("#idCandidate").length) {
        experienciaData["Id"] = $("#idCandidate").val();
        url = `${url}/${$("#idCandidate").val()}`;
    }

    // Realizar la solicitud POST al API
    guardarCandidato(url, experienciaData);
});

function guardarCandidato(url, experienciaData) {
    // Convierte el objeto JSON en una cadena JSON
    var experienciaJSON = JSON.stringify(experienciaData);
    // Realizar la solicitud POST al API
    $.ajax({
        url: url, // Cambia la URL a la ruta correcta de tu API
        method: ($("#idCandidate").length && $("#idCandidate").val() != "") ? "PUT" : "POST",
        data: experienciaJSON,
        contentType: "application/json", // Especifica el tipo de contenido como JSON
        dataType: "json",
        success: function (data) {
            // Cerrar el modal después de guardar
            modalCandidato.modal("hide");

            Swal.fire({
                html: `Los cambios fueron realizados`,
                icon: "success"
            });

            if ($("#idCandidate").length) {
                localStorage.removeItem("idCandidate");
                formCandidato.find("#idCandidate").remove();
            }

            // Puedes actualizar la tabla o recargar los datos aquí si es necesario
            cargarDatos()
        },
        error: function (e) {
            console.error(e.responseText);
            Swal.fire({
                html: `Error al guardar el candidato. <b>${e.responseText}</b>`,
                icon: "error"
            });
        }
    });
}

$(".cerrarModales").on("click", function() {
    localStorage.removeItem("idCandidate");
    formCandidato.find("#idCandidate").remove();
    //$("#idCandidate").remove();
})
