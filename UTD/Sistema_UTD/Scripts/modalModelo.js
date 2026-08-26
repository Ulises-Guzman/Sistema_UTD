function abrirModalModelo() {
    // 1. Buscar el elemento HTML del modal
    var elementoModal = document.getElementById('frmModalModelo');

    // 2. Usar getOrCreateInstance para evitar crear modales duplicados en memoria
    var miModal = bootstrap.Modal.getOrCreateInstance(elementoModal);

    // 3. Mostrar
    miModal.show();
}

function cerrarModalModelo() {
    // 1. Buscar el elemento HTML del modal
    var elementoModal = document.getElementById('frmModalModelo');

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