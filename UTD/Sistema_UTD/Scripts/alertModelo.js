'use strict'

var prm = Sys.WebForms.PageRequestManager.getInstance();

prm.add_endRequest(function () {
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
            }, 4000);
        }
    });
});