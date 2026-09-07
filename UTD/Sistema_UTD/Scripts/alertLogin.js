'use strict'

document.addEventListener("DOMContentLoaded", function () {
    var alertas = document.querySelectorAll('.alert');

    alertas.forEach(function (alerta) {
        // Solo trabajamos con las alertas que estén visibles
        if (alerta.style.display !== 'none') {

            // Temporizador principal de 4 segundos
            setTimeout(function () {

                // PASO 1: Quitamos la clase 'show'. 
                // Esto inicia el efecto visual suave (fade out) de Bootstrap
                alerta.classList.remove('show');

                // PASO 2: Le damos tiempo a que termine el efecto antes de quitarlo del medio
                // Bootstrap tarda aprox 150ms en hacer el fade. Le damos 200ms por seguridad.
                setTimeout(function () {
                    alerta.style.display = 'none';

                    // Volvemos a agregar la clase 'show' ocultamente para que 
                    // cuando C# lo vuelva a encender en el futuro, aparezca correctamente.
                    alerta.classList.add('show');
                }, 200);

            }, 4000); // Ajustar este número para dar más o menos tiempo
        }
    });
});