class Input {
    constructor() {}
    getEmptyInputs(elements) {
        let emptyInputs = [];
        const regexEmpty = /^[\s]*$/;
        for (let i = 0; i < elements.length; i++) {
            let empty = false;
            if (regexEmpty.test(elements[i].value)){
                emptyInputs.push(elements[i]);
            }
        }
        return emptyInputs;
    }
    displayErrorForEmptyInputs(inputs) {
        for (let i = 0; i < inputs.length; i++) {
            inputs[i].nextSibling.textContent = "can`t be empty";
            event.preventDefault();
        }
    }
    clearErrorForInputs(inputs) {
        for (let i = 0; i < inputs.length; i++) {
            inputs[i].nextSibling.textContent = "";
        }
    }
}
