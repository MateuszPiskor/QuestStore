function toggleMenu(e) {
    const menuClass = "sidemenu--visible";
    let menu = document.querySelector(".sidemenu");

    if (menu.classList.contains(menuClass)) {
        menu.classList.remove(menuClass);
    } else {
        menu.classList.add(menuClass);
    }
}