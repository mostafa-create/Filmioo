document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('registerForm');
    const fNameInput = document.getElementById('FName'); 
    const lNameInput = document.getElementById('LName');
    const emailAddressInput = document.getElementById('EmailAddress');
    const passwordInput = document.getElementById('Password');
    const confirmPasswordInput = document.getElementById('ConfirmPassword');
    const isAgreeInput = document.getElementById('IsAgree');

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    function displayCustomError(element, message) {
        const validationSpan = document.querySelector(`span[data-valmsg-for='${element.id}']`);
        if (validationSpan) {
            validationSpan.textContent = message;
            element.classList.add('input-error'); 
        }
    }
    function clearCustomError(element) {
        const validationSpan = document.querySelector(`span[data-valmsg-for='${element.id}']`);
        if (validationSpan) {
            validationSpan.textContent = '';
            element.classList.remove('input-error');
        }
    }

    form.addEventListener('submit', (event) => {
        let isValid = form.checkValidity(); 

        if (passwordInput.value !== confirmPasswordInput.value) {
            displayCustomError(confirmPasswordInput, "Password Doesn't Match");
            isValid = false;
        } else {
            clearCustomError(confirmPasswordInput);
        }

        if (!isAgreeInput.checked) {
            displayCustomError(isAgreeInput, 'You must agree to the terms.');
            isValid = false;
        } else {
            clearCustomError(isAgreeInput);
        }

        if (!isValid) {
            event.preventDefault(); 

        } else {
            const button = document.querySelector('.register-button');
            button.textContent = 'Registering...';
            button.disabled = true;
        }
    });

    [passwordInput, confirmPasswordInput].forEach(input => {
        input.addEventListener('input', () => {
            if (passwordInput.value !== confirmPasswordInput.value && confirmPasswordInput.value.length > 0) {
                displayCustomError(confirmPasswordInput, "Password Doesn't Match");
            } else {
                clearCustomError(confirmPasswordInput);
            }
        });
    });
});