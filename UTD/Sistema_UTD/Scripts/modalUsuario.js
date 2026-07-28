function abrirModalUsuario() {
    // 1. Buscar el elemento HTML del modal
    var elementoModal = document.getElementById('frmModalUsuario');

    // 2. Usar getOrCreateInstance para evitar crear modales duplicados en memoria
    var miModal = bootstrap.Modal.getOrCreateInstance(elementoModal);

    // 3. Mostrar
    miModal.show();
}

function cerrarModalUsuario() {
    // 1. Buscar el elemento HTML del modal
    var elementoModal = document.getElementById('frmModalUsuario');

    // 2. Obtener la instancia que ya está abierta
    var miModal = bootstrap.Modal.getInstance(elementoModal);

    // 3. Cerrar
    if (miModal) {
        miModal.hide(); // Aquí limpio el modal-backDrop
    }
}

function limpiarFondoModal() {
    // 1. Buscar y destruir todos los fondos grises que hayan quedado
    var fondosGrises = document.querySelectorAll('.modal-backdrop');
    fondosGrises.forEach(function (fondo) {
        fondo.remove();
    });

    // 2. Devolver al body la capacidad de hacer scroll
    document.body.classList.remove('modal-open');
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
}

// function cambiarCampos(chkUsuarioActivo) {
//     var txtUsuario = document.getElementById('txtUsuario');
//     var ddlRol = document.getElementById('ddlRol');
//     var txtApellido = document.getElementById('txtApellido');
//     var txtNombre = document.getElementById('txtNombre');

//     if (!chkUsuarioActivo.checked) {
//         txtUsuario.disabled = true;
//         ddlRol.disabled = true;
//         txtApellido.disabled = true;
//         txtNombre.disabled = true;
//     } else {
//         txtUsuario.disabled = false;
//         ddlRol.disabled = false;
//         txtApellido.disabled = false;
//         txtNombre.disabled = false;
//     }
// }

// function cambiarCampoContrasenia(chkCambioContrasenia) {
//     var txtContrasenia = document.getElementById('txtContrasenia');

//     if (!chkCambioContrasenia.checked) {
//         txtContrasenia.disabled = true;
//     } else {
//         txtContrasenia.disabled = false;
//     }
// }