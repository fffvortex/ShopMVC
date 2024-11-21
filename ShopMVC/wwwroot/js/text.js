
var wrapper = document.querySelector(".typing");
var text = document.querySelector(".typing .text");
var textCont = text.textContent;
text.style.display = "none";
for (var i = 0; i < textCont.length; i++) {
    (function (i) {
        setTimeout(function () {
            var texts = document.createTextNode(textCont[i])
            var span = document.createElement('span');
            span.appendChild(texts);
            wrapper.appendChild(span);
        }, 26 * i);
    }(i));
}