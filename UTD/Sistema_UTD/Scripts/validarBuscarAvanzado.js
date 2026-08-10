'use strict';

// 1. Declaramos las variables globales
let txtBuscarAvanzado;

// 2. Creamos una función que "engancha" los eventos.
// Esta función se llamará al inicio y DESPUÉS de cada recarga del UpdatePanel.
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtBuscarAvanzado = document.getElementById("txtBuscarAvanzado");

    // Metemos los campos en un arreglo para no repetir código
    let campo = txtBuscarAvanzado;

    // Verifico el campo y le asignamos los eventos
        if (campo) {
            // Evento 'input': Valida mientras el usuario teclea
            campo.addEventListener('input', () => validarCampo(campo));

            // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
            campo.addEventListener('blur', () => validarCampo(campo));
        }
    
}

// 3. ¡LA MAGIA DE ASP.NET!
// Si existe el ScriptManager de ASP.NET, usamos su método de carga
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    // Si no hay UpdatePanel, usamos el método tradicional de HTML
    //document.addEventListener("DOMContentLoaded", enlazarEventos);
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
function validarUsuarioBuscado() {
    // Forzamos la actualización de variables por seguridad antes de validar
    enlazarEventos();

    const BuscarAvanzadoValido = validarCampo(txtBuscarAvanzado);
  
    // Si alguno es falso, retorna false (detiene el guardado)
    if (!BuscarAvanzadoValido) {
        return false;
    }

    return true; // Todo ok, permite que viaje a C#
}