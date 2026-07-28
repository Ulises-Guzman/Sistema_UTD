'use strict';

// 1. Declaramos las variables globales
let txtUsuario, txtContrasenia, ddlRol, txtApellido, txtNombre;

// 2. Función para enganchar eventos (preparada para el UpdatePanel)
function enlazarEventos() {
    txtUsuario = document.getElementById("txtUsuario");
    txtContrasenia = document.getElementById("txtContrasenia");
    ddlRol = document.getElementById("ddlRol");
    txtApellido = document.getElementById("txtApellido");
    txtNombre = document.getElementById("txtNombre");

    let campos = [txtUsuario, txtContrasenia, ddlRol, txtApellido, txtNombre];

    campos.forEach(campo => {
        if (campo) {
            campo.addEventListener('input', () => validarCampo(campo));
            campo.addEventListener('blur', () => validarCampo(campo));
        }
    });
}

// 3. Manejo del ciclo de vida de ASP.NET
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    document.addEventListener("DOMContentLoaded", enlazarEventos);
}

// 4. NUEVA LÓGICA: Función que maneja el switch general (Usuario Activo/Inactivo)
function cambiarCampos(checkbox) {
    let camposGenerales = [txtUsuario, ddlRol, txtApellido, txtNombre];
    let estaActivo = checkbox.checked;

    // Habilitar o deshabilitar campos generales
    camposGenerales.forEach(campo => {
        if (campo) {
            campo.disabled = !estaActivo;
            // Si acabamos de deshabilitarlo, le quitamos los colores de validación
            if (!estaActivo) {
                campo.classList.remove("is-valid", "is-invalid");
            }
        }
    });

    // Lógica adicional de seguridad: Si desactivo al usuario, el switch de contraseña
    // también debería desactivarse para no dejar cabos sueltos.
    let chkPass = document.getElementById("chkCambioContrasenia");
    if (chkPass) {
        if (!estaActivo) {
            chkPass.checked = false;       // Le quitamos el check
            chkPass.disabled = true;       // Lo bloqueamos
            cambiarCampoContrasenia(chkPass); // Llamamos a su función para que limpie el TextBox
        } else {
            chkPass.disabled = false;      // Lo volvemos a habilitar si el usuario está activo
        }
    }
}

// 5. NUEVA LÓGICA: Función que maneja el switch de la Contraseña
function cambiarCampoContrasenia(checkbox) {
    if (txtContrasenia) {
        // Habilita o deshabilita el TextBox de la contraseña
        txtContrasenia.disabled = !checkbox.checked;

        // Si el usuario apaga el switch, limpiamos los colores y borramos lo que escribió
        if (!checkbox.checked) {
            txtContrasenia.classList.remove("is-valid", "is-invalid");
            txtContrasenia.value = ""; // Vaciamos el campo por seguridad
        }
    }
}

// 6. Validación individual modificada
function validarCampo(campo) {
    if (!campo) return false;

    // REGLA CLAVE: Si el campo está deshabilitado, es válido por defecto y no lleva colores
    if (campo.disabled) {
        campo.classList.remove("is-valid", "is-invalid");
        return true;
    }

    // Validamos normal si el campo está habilitado
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

// 7. Función principal para el botón Aceptar
function validar() {
    enlazarEventos(); // Forzamos actualización de variables

    const usuarioValido = validarCampo(txtUsuario);
    const contraseniaValido = validarCampo(txtContrasenia);
    const rolValido = validarCampo(ddlRol);
    const nombreValido = validarCampo(txtNombre);
    const apellidoValido = validarCampo(txtApellido);

    // Si ALGUNO es falso (y no estaba deshabilitado), bloqueamos el guardado
    if (!usuarioValido || !contraseniaValido || !rolValido || !nombreValido || !apellidoValido) {
        return false;
    }

    return true; // Todo en orden
}