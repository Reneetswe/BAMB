   <script>
        // Cart functionality
        let cartCount = 0;
        const cartBadge = document.querySelector('.cart-badge');
        const addToCartButtons = document.querySelectorAll('.add-to-cart');

        addToCartButtons.forEach(button => {
            button.addEventListener('click', () => {
                cartCount++;
                cartBadge.setAttribute('data-count', cartCount);
                
                // Animation effect
                button.innerHTML = '<i class="fas fa-check"></i> Added';
                button.style.background = '#27ae60';
                
                setTimeout(() => {
                    button.innerHTML = '<i class="fas fa-shopping-cart"></i> Add to Cart';
                    button.style.background = '';
                }, 1500);
            });
        });

        // Favorite functionality
        const favoriteButtons = document.querySelectorAll('.favorite-btn');
        favoriteButtons.forEach(button => {
            button.addEventListener('click', () => {
                const icon = button.querySelector('i');
                if (icon.classList.contains('far')) {
                    icon.classList.remove('far');
                    icon.classList.add('fas');
                    icon.style.color = '#e74c3c';
                } else {
                    icon.classList.remove('fas');
                    icon.classList.add('far');
                    icon.style.color = '';
                }
            });
        });

        // Category selection
        const categoryButtons = document.querySelectorAll('.category-btn');
        categoryButtons.forEach(button => {
            button.addEventListener('click', () => {
                categoryButtons.forEach(btn => btn.classList.remove('active'));
                button.classList.add('active');
            });
        });

        // Search functionality
        const searchInput = document.querySelector('.search-bar input');
        searchInput.addEventListener('input', (e) => {
            const searchTerm = e.target.value.toLowerCase();
            const productTitles = document.querySelectorAll('.product-title');
            
            productTitles.forEach(title => {
                const card = title.closest('.product-card');
                const productName = title.textContent.toLowerCase();
                
                if (productName.includes(searchTerm)) {
                    card.style.display = '';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    </script>