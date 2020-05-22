function toggleMenu(e) {
    const menuClass = "sidemenu--visible";
    let menu = document.querySelector(".sidemenu");

    if (menu.classList.contains(menuClass)) {
        menu.classList.remove(menuClass);
    } else {
        menu.classList.add(menuClass);
    }
}

window.onload = function () {
    let form = document.querySelector(".form");
    form.addEventListener("submit",this.ValidateEmptyInputs)
};



function ValidateEmptyInputs() {
    const input = new Input();

    let inputs = document.querySelectorAll(".form__input");
    let emptyInputs = input.getEmptyInputs(inputs);

    input.clearErrorForInputs(inputs);
    input.displayErrorForEmptyInputs(emptyInputs);
}