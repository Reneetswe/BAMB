<script>
        // Cart functionality
        document.querySelectorAll('.quantity-btn').forEach(button => {
            button.addEventListener('click', function() {
                const quantitySpan = this.parentElement.querySelector('span');
                let quantity = parseInt(quantitySpan.textContent);
                
                if (this.textContent === '+') {
                    quantity++;
                } else if (this.textContent === '-' && quantity > 1) {
                    quantity--;
                }
                
                quantitySpan.textContent = quantity;
                updateTotal();
            });
        });

        document.querySelectorAll('.remove-btn').forEach(button => {
            button.addEventListener('click', function() {
                this.closest('.cart-item').remove();
                updateTotal();
                
                // Update cart count in nav
                const cartItems = document.querySelectorAll('.cart-item').length;
                document.querySelector('.nav-links a.active').innerHTML = 
                    `<i class="fas fa-shopping-cart"></i> Cart (${cartItems})`;
                
                if (cartItems === 0) {
                    showEmptyCart();
                }
            });
        });

        function showEmptyCart() {
            const cartItems = document.querySelector('.cart-items');
            cartItems.innerHTML = `
                <div class="empty-cart">
                    <i class="fas fa-shopping-basket"></i>
                    <h2>Your cart is empty</h2>
                    <p>Looks like you haven't added any items to your cart yet.</p>
                    <a href="#marketplace" class="continue-shopping">Continue Shopping</a>
                </div>
            `;
            document.querySelector('.cart-summary').style.display = 'none';
        }

        function updateTotal() {
            // In a real application, this would calculate based on actual prices
            const items = document.querySelectorAll('.cart-item').length;
            const subtotal = items * 250; // Example calculation
            const delivery = 100;
            const total = subtotal + delivery;

            document.querySelector('.summary-item:nth-child(1) span:last-child').textContent = 
                `P ${subtotal.toFixed(2)}`;
            document.querySelector('.summary-item:last-child strong:last-child').textContent = 
                `P ${total.toFixed(2)}`;
        }
    </script>