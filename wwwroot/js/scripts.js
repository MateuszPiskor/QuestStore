function toggleMenu(e) {
    const menuClass = "sidemenu--visible";
    let menu = document.querySelector(".sidemenu");

    if (menu.classList.contains(menuClass)) {
        menu.classList.remove(menuClass);
    } else {
        menu.classList.add(menuClass);
    }
}

window.addEventListener('load', function() {
    console.log("Document loaded.");
    let forms = document.querySelectorAll(".form");
    if (forms) {
        for (let i = 0; i < forms.length; i++) {
            forms[i].addEventListener("submit", this.ValidateEmptyInputs); 
            console.log("Attached handler to form.");
        }
    }
});

function ValidateEmptyInputs() {
    const input = new Input();

    let inputs = document.querySelectorAll(".form__input");
    let emptyInputs = input.getEmptyInputs(inputs);

    input.clearErrorForInputs(inputs);
    input.displayErrorForEmptyInputs(emptyInputs);
}