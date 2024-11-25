
let items = document.getElementsByClassName('btn_character');



for (let i = 0; i <= items.length+1; i++) {
    items[i].onclick = function () {
        items[i].classList.toggle('es')
        
        var classname = items[i].className.replace(/\D/g, '')
        stats = document.getElementById(classname)

        if (stats.classList.contains('openCharacter')) {
            stats.classList.remove('openCharacter')
        }
        else {
            stats.classList.add('openCharacter')
        }
    }
}

