

var swiper = new Swiper(".swiper", {
    initialSlide: 2,
    allowSlideNext: true,
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: "auto",
    autoplay: {
        delay: 3000,
    },
    speed : 500,
    coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 400,
        slideShadows: true,
    },
    pagination: {
        el: ".swiper-pagination" },
    })