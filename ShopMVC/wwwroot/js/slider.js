let swiper = new Swiper(".swiper", {
    centeredSlides: true,
    effect: "coverflow",
    grabCursor: true,
    slidesPerView: 'auto',
    loop: true,
    autoplay: {
        delay: 2000,
    },
    speed: 500,
    coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 400,
        slideShadows: true,
    }
})