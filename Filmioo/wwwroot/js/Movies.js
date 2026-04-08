document.addEventListener('DOMContentLoaded', () => {

    const movieCardLinks = document.querySelectorAll('.filmioo-card-link');

    movieCardLinks.forEach(link => {
        const card = link.querySelector('.filmioo-card');

        link.addEventListener('mousedown', function () {
            card.classList.add('active-pulse');
        });

        link.addEventListener('mouseup', function () {
            setTimeout(() => {
                card.classList.remove('active-pulse');
            }, 100);
        });
    });
});