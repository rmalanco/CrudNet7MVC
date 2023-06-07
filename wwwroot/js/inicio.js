$(document).ready(function () {
  //Llamar a datatable y generar la tabla de datos
  $("#tblUsuarios").DataTable({
    ajax: {
      url: "/Inicio/GetAll",
    },
    columns: [
      { data: "nombre" },
      { data: "telefono" },
      { data: "celular" },
      { data: "email" },
      { data: "fechaCreacion" },
      {
        data: "id",
        render: function (data) {
          return (
            '<div class="btn-group" role="group" aria-label="Basic example">' +
            '<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalCrearEditarDetalles" data-id="' +
            data +
            '"><i class="bi bi-pencil-square"></i></button>' +
            '<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#modalEliminar" data-id="' +
            data +
            '"><i class="bi bi-trash"></i></button>' +
            "</div>"
          );
        },
      },
    ],
    language: {
      decimal: "",
      emptyTable: "No hay información",
      info: "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
      infoEmpty: "Mostrando 0 to 0 of 0 Entradas",
      infoFiltered: "(Filtrado de _MAX_ total entradas)",
      infoPostFix: "",
      thousands: ",",
      lengthMenu: "Mostrar _MENU_ Entradas",
      loadingRecords: "Cargando...",
      processing: "Procesando...",
      search: "Buscar:",
      zeroRecords: "Sin resultados encontrados",
      paginate: {
        first: "Primero",
        last: "Ultimo",
        next: "Siguiente",
        previous: "Anterior",
      },
    },
  });

  $("#modalCrearEditarDetalles").on("show.bs.modal", function (event) {
    var id = $(event.relatedTarget).data("id");
    var modal = $(this);
    if (id > 0) {
      modal.find(".modal-title").text("Editar Contacto");
      $.ajax({
        url: "/Inicio/GetById",
        data: { id: id },
        type: "POST",
        success: function (data) {
          let contacto = data.data;
          modal.find("#id").val(contacto.id);
          modal.find("#nombre").val(contacto.nombre);
          modal.find("#telefono").val(contacto.telefono);
          modal.find("#celular").val(contacto.celular);
          modal.find("#email").val(contacto.email);
        },
      });
    } else {
      modal.find(".modal-title").text("Crear Contacto");
      modal.find("#id").val(0);
      modal.find("#nombre").val("");
      modal.find("#telefono").val("");
      modal.find("#celular").val("");
      modal.find("#email").val("");
    }
  });

  $("#modalEliminar").on("show.bs.modal", function (event) {
    var id = $(event.relatedTarget).data("id");
    $("#idEliminar").val(id);
  });

  $("#btnEliminar").click(function (event) {
    let id = $("#idEliminar").val();
    let modal = $("#modalEliminar");
    $.ajax({
      url: "/Inicio/Delete",
      data: { id: id },
      type: "POST",
      success: function (data) {
        modal.find("#btnCerrarEliminar").click();
        $("#tblUsuarios").DataTable().ajax.reload();
        Swal.fire({
          icon: "success",
          title: data.message,
          showConfirmButton: false,
          timer: 1500,
        });
      },
    });
  });

  $("#btnGuardar").click(function (event) {
    event.preventDefault();
    let modal = $("#modalCrearEditarDetalles");
    let id = $("#id").val();
    let nombre = $("#nombre").val();
    let telefono = $("#telefono").val();
    let celular = $("#celular").val();
    let email = $("#email").val();
    if (id > 0) {
      $.ajax({
        url: "/Inicio/Update",
        data: {
          id: id,
          nombre: nombre,
          telefono: telefono,
          celular: celular,
          email: email,
        },
        type: "POST",
        success: function (data) {
          if (data.success) {
            modal.find("#btnCancelar").click();
            $("#tblUsuarios").DataTable().ajax.reload();
            Swal.fire({
              icon: "success",
              title: data.message,
              showConfirmButton: false,
              timer: 1500,
            });
          } else {
            var errorMessages = "";
            $.each(data.errors, function (key, value) {
              errorMessages += value + "<br>";
            });
            Swal.fire({
              icon: "error",
              title: data.message,
              html: errorMessages,
            });
          }
        },
      });
    } else {
      $.ajax({
        url: "/Inicio/Create",
        data: {
          nombre: nombre,
          telefono: telefono,
          celular: celular,
          email: email,
        },
        type: "POST",
        success: function (data) {
          if (data.success) {
            modal.find("#btnCancelar").click();
            $("#tblUsuarios").DataTable().ajax.reload();
            Swal.fire({
              icon: "success",
              title: data.message,
              showConfirmButton: false,
              timer: 1500,
            });
          } else {
            var errorMessages = "";
            $.each(data.errors, function (key, value) {
              errorMessages += value + "<br>";
            });
            Swal.fire({
              icon: "error",
              title: data.message,
              html: errorMessages,
            });
          }
        },
      });
    }
  });
});
