'use strict';

// Declaro las variables fuera para que toda la página las pueda usar
let txtUsuario;
let txtContrasenia;

// 1. Esperar a que el HTML (DOM) cargue completamente
document.addEventListener("DOMContentLoaded", function () {

    // Ahora es seguro capturar los controles porque sabemos que ya existen en la página
    // Gracias a ClientIDMode="Static", los IDs son exactamente los mismos
    txtUsuario = document.getElementById("txtUsuario");
    txtContrasenia = document.getElementById("txtContrasenia");

    // Asignar el evento 'input' para que valide en tiempo real mientras el usuario escribe
    if (txtUsuario) {
        txtUsuario.addEventListener('input', () => validarCampo(txtUsuario));
        txtUsuario.addEventListener('blur', () => validarCampo(txtUsuario));
    }
    if (txtContrasenia) {
        txtContrasenia.addEventListener('input', () => validarCampo(txtContrasenia));
        txtContrasenia.addEventListener('blur', () => validarCampo(txtContrasenia));
    }
})


// 2. Crear una función genérica que valide un solo campo a la vez
function validarCampo(campo) {
    if (campo.value.trim() === "") {
        campo.classList.remove("is-valid");
        campo.classList.add("is-invalid");
        return false; // Retorna falso si hay error
    } else {
        campo.classList.remove("is-invalid");
        campo.classList.add("is-valid");
        return true; // Retorna verdadero si está todo bien
    }
}


// 4. Función principal para llamarla cuando se hace click en el botón "Guardar""
function validarLogin() {
    // Valido todos los campos
    const usuarioValido = validarCampo(txtUsuario);
    const contraseniaValido = validarCampo(txtContrasenia);

    // Si alguno es falso la validación general falla
    if (!usuarioValido || !contraseniaValido) {
        return false;
    }
    return true;
}