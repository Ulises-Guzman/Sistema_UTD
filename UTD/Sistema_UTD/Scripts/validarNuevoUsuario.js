'use strict';

// 1. Declaramos las variables globales
let txtUsuario, txtContrasenia, ddlRol, txtApellido, txtNombre;

// 2. Creamos una función que "engancha" los eventos.
// Esta función se llamará al inicio y DESPUÉS de cada recarga del UpdatePanel.
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtUsuario = document.getElementById("txtUsuario");
    txtContrasenia = document.getElementById("txtContrasenia");
    ddlRol = document.getElementById("ddlRol");
    txtApellido = document.getElementById("txtApellido");
    txtNombre = document.getElementById("txtNombre");

    // Metemos los campos en un arreglo para no repetir código
    let campos = [txtUsuario, txtContrasenia, ddlRol, txtApellido, txtNombre];

    // Recorremos cada campo y le asignamos los eventos
    campos.forEach(campo => {
        if (campo) {
            // Evento 'input': Valida mientras el usuario teclea
            campo.addEventListener('input', () => validarCampo(campo));

            // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
            campo.addEventListener('blur', () => validarCampo(campo));
        }
    });
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
function validar() {
    // Forzamos la actualización de variables por seguridad antes de validar
    enlazarEventos();

    const usuarioValido = validarCampo(txtUsuario);
    const contraseniaValido = validarCampo(txtContrasenia);
    const rolValido = validarCampo(ddlRol);
    const nombreValido = validarCampo(txtNombre);
    const apellidoValido = validarCampo(txtApellido);

    // Si alguno es falso, retorna false (detiene el guardado)
    if (!usuarioValido || !contraseniaValido || !rolValido || !nombreValido || !apellidoValido) {
        return false;
    }

    return true; // Todo ok, permite que viaje a C#
}