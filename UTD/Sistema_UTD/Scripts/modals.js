function abrirModalABM() {
    // Inicializa y muestra el modal usando la API de Bootstrap 5
    //var miModal = new bootstrap.Modal(document.getElementById('frmModalABM'));
    //miModal.show();

    // 1. Buscamos el elemento HTML del modal
    var elementoModal = document.getElementById('frmModalABM');

    // 2. Usamos getOrCreateInstance para evitar crear modales duplicados en memoria
    var miModal = bootstrap.Modal.getOrCreateInstance(elementoModal);

    // 3. Lo mostramos
    miModal.show();
}