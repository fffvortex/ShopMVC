var swiper = new Swiper(".swiper", {
    effect: "coverflow",
    grabCursor: true,
    loop: true,
    centeredSlides: true,
    slidesPerView: 'auto',
    autoplay: {
        delay: 3000,
    },
    speed: 500,
    coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 400,
        slideShadows: true,
    },
    pagination: {
        el: ".swiper-pagination"
    }
})