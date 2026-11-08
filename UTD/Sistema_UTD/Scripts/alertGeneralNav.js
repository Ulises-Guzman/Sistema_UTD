'use strict'

// Para usar en navegación entre páginas
function ocultarNotificaciones() {
    var alertas = document.querySelectorAll('.alert');

    alertas.forEach(function (alerta) {
        if (alerta.style.display !== 'none') {

            // Fuerza a la pantalla a desplazarse suavemente hasta la alerta
            alerta.scrollIntoView({ behavior: 'smooth', block: 'center' });

            setTimeout(function () {
                alerta.classList.remove('show');
                setTimeout(function () {
                    alerta.style.display = 'none';
                    alerta.classList.add('show');
                }, 200);
            }, 6000);
        }
    });
}

// Si existe el ScriptManager de ASP.NET, usamos su método de carga global
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(ocultarNotificaciones);
} else {
    // Si no hay UpdatePanel, usamos el método tradicional de HTML
    document.addEventListener("DOMContentLoaded", ocultarNotificaciones);
}