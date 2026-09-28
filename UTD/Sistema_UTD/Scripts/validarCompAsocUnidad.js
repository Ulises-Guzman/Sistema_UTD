'use strict';

// 1. Declaramos las variables globales
let txtUnidad;

// 2. Creamos una función que "engancha" los eventos.
// Esta función se llamará al inicio y DESPUÉS de cada recarga del UpdatePanel.
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtUnidad = document.getElementById("txtUnidad");

    // Valida si exite el campo
    if (txtUnidad) {
        // Evento 'input': Valida mientras el usuario teclea
        txtUnidad.addEventListener('input', () => validarCampo(txtUnidad));

        // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
        txtUnidad.addEventListener('blur', () => validarCampo(txtUnidad));
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
function validarUnidad() {
    // Forzamos la actualización de variables por seguridad antes de validar
    enlazarEventos();

    const unidadValido = validarCampo(txtUnidad);

    // Si alguno es falso, retorna false (detiene el guardado)
    if (!unidadValido) {
        return false;
    }

    return true; // Todo ok, permite que viaje a C#
}