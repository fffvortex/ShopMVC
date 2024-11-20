
const toggleBtn = document.querySelector('.toggle_btn')

const toggleBtnIcon = document.querySelector('.toggle_btn i')

const toggleDownMenu = document.querySelector('.dropdown_menu')

toggleBtn.onmouseover = function () {
    toggleBtnIcon.classList.add('fa-flip')
}
toggleBtn.onmouseout = function () {
    toggleBtnIcon.classList.remove('fa-flip')
}
toggleBtn.onclick = function() {
    toggleDownMenu.classList.toggle('open')
    console.log("click");

     const isOpen = toggleDownMenu.classList.contains('open')

     toggleBtnIcon.classList = isOpen ? 'fa-sharp fa-solid fa-xmark style="color: #2fdd2c;': 'fa-sharp fa-solid fa-bars style="color: #2fdd2c;'
}

