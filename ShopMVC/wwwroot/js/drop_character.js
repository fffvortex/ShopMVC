
let items = document.getElementsByClassName('btn_character');

for (let i = 0; i <= items.length + 1; i++) {
    items[i].onclick = function () {
        items[i].classList.contains('es') ? items[i].classList.remove('es') : items[i].classList.add('es');

        var classname = items[i].className.replace(/\D/g, '')
        stats = document.getElementById(classname)

        stats.classList.contains('openCharacter') ? stats.classList.remove('openCharacter') : stats.classList.add('openCharacter')
    }
}