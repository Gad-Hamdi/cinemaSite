// Smooth scrolling for anchor links
document.addEventListener('DOMContentLoaded', function () {
    // Smooth scroll for back to top functionality
    const scrollToTop = document.querySelector('.scroll-to-top');
    if (scrollToTop) {
        scrollToTop.addEventListener('click', function (e) {
            e.preventDefault();
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    // Add loading animation to buttons
    const buttons = document.querySelectorAll('.btn-details, .page-link');
    buttons.forEach(button => {
        button.addEventListener('click', function () {
            this.classList.add('loading');
            setTimeout(() => {
                this.classList.remove('loading');
            }, 1000);
        });
    });

    // Search form enhancement
    const searchForm = document.querySelector('.search-container form');
    if (searchForm) {
        const searchInput = searchForm.querySelector('.search-input');
        searchInput.addEventListener('focus', function () {
            searchForm.parentElement.classList.add('focused');
        });
        searchInput.addEventListener('blur', function () {
            searchForm.parentElement.classList.remove('focused');
        });
    }
});

// Image lazy loading
if ('IntersectionObserver' in window) {
    const lazyImages = document.querySelectorAll('img[data-src]');

    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.classList.remove('lazy');
                imageObserver.unobserve(img);
            }
        });
    });

    lazyImages.forEach(img => {
        imageObserver.observe(img);
    });
}