'use strict';

// 1. Declaramos las variables globales
let txtModelo;

// 2. Creamos una función que "engancha" los eventos.
// Esta función se llamará al inicio y DESPUÉS de cada recarga del UpdatePanel.
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtModelo = document.getElementById("txtModelo");

    // Valida si exite el campo
    if (txtModelo) {
        // Evento 'input': Valida mientras el usuario teclea
        txtModelo.addEventListener('input', () => validarCampo(txtModelo));

            // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
        txtModelo.addEventListener('blur', () => validarCampo(txtModelo));
        }
}

// 3. ¡LA MAGIA DE ASP.NET!
// Si existe el ScriptManager de ASP.NET, usamos su método de carga
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    // Si no hay UpdatePanel, usamos el método tradicional de HTML
    document.addEventListener("DOMContentLoaded", enlazarEventos);
}

// 4. Función genérica que valida un solo campo
function validarCampo(campo) {
    // Si el campo no existe por alguna razón, no hacemos nada
    if (!campo) return false;

    if (campo.value.trim() === "") {
        campo.classList.remove("is-valid");
        campo.classList.add("is-invalid");
        return false;
    } else {
        campo.classList.remove("is-invalid");
        campo.classList.add("is-valid");
        return true;
    }
}

// 5. Función principal para el botón Guardar
function validarModeloNuevo() {
    // Forzamos la actualización de variables por seguridad antes de validar
    enlazarEventos();

    const modeloValido = validarCampo(txtModelo);

    // Si alguno es falso, retorna false (detiene el guardado)
    if (!modeloValido) {
        return false;
    }

    return true; // Todo ok, permite que viaje a C#
}