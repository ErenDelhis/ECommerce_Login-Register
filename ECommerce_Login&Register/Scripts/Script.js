document.addEventListener('DOMContentLoaded', function () {
    const cartItems = [];
    const cartToggleBtn = document.getElementById('cart-toggle-btn');
    const cartPanel = document.getElementById('cart-panel');
    const cartCloseBtn = document.getElementById('cart-close-btn');
    const cartItemsList = document.getElementById('cart-items-list');
    const cartTotal = document.querySelector('.cart-total');
    const clearCartBtn = document.getElementById('clear-cart-btn');
    const acceptBtn = document.getElementById('accept-btn');
    const checkBalanceBtn = document.getElementById('checkBalanceBtn');
    const addToCartButtons = document.querySelectorAll('.add-to-cart');
    // Sepeti Göster
    cartToggleBtn.addEventListener('click', function (event) {
        cartPanel.classList.toggle('show');
    });
    // Sepeti Kapat
    cartCloseBtn.addEventListener('click', function () {
        cartPanel.classList.remove('show');
    });
    // Sepete ekleme ve Onaylama yeri
    if (checkBalanceBtn) {
        // Bakiye kontrol
        checkBalanceBtn.addEventListener('click', function () {
            $.ajax({
                url: '/Home/CheckBalance',
                method: 'GET',
                success: function (response) {
                    alert('Bakiyeniz: $' + response.toFixed(2));
                },
                error: function (error) {
                    console.error('Hata:', error);
                    alert('Sanırım daha bakiye hesabı açmadınız');
                }
            });
        });
    } else {
        console.error("checkBalanceBtn bulunamadı!"); // Hata kontrolü için
    }
    acceptBtn.addEventListener('click', function () {
        // Kullanıcı giriş yapmış mı kontrol et
        var isLoggedIn = document.body.getAttribute('data-logged-in') === 'True';
        if (!isLoggedIn) {
            // Kullanıcı giriş yapmamışsa uyarı göster ve IndexLogin sayfasına yönlendir
            alert('Öncelikle giriş yapınız.');
            window.location.href = '/Home/IndexLogin';
        } else {
            // Kullanıcı giriş yapmışsa sepeti onayla 
            if (cartItems.length === 0) {
                alert('Lütfen ürün seçiniz.');
                cartPanel.classList.remove('show');
                return;
            }
            let totalPrice = cartItems.reduce((total, item) => total + parseFloat(item.price) * item.quantity, 0);
            $.ajax({
                url: '/Home/CheckBalance',
                method: 'GET',
                success: function (response) {
                    if (response >= totalPrice) {
                        try {
                            // Kullanıcının bakiyesini azalt
                            $.ajax({
                                url: '/Home/ReduceBalance',
                                method: 'POST',
                                data: { amount: totalPrice },
                                success: function () {
                                    clearCart();
                                    alert('Sepet Onaylandı');
                                    cartPanel.classList.remove('show');
                                },
                                error: function (error) {
                                    console.error('Hata:', error);
                                    alert('Bir hata oluştu. Sepet Onaylanmadı.!!');
                                }
                            });
                        } catch (error) {
                            console.error('Hata:', error);
                            alert('Bir hata oluştu. Lütfen tekrar deneyin.');
                        }
                    } else {
                        // Kullanıcının bakiyesi yetersiz ise uyarı göster
                        alert('Yetersiz bakiye!');
                    }
                },
                error: function (error) {
                    console.error('Hata:', error);
                    alert('Bir hata oluştu. Lütfen tekrar deneyin.');
                }
            });
        }
    });
    function clearCart() {
        if (cartItems.length === 0) {
            alert('İlk önce ürün seçiniz.');
            return;
        }
        // Sepeti temizle
        cartItems.splice(0, cartItems.length);
        // Sepet panelini güncelle
        updateCartPanel();
    }
    // Sepeti göster ve ürün ekle
    function updateCartPanel() {
        // Sepetin içeriğini temizle
        cartItemsList.innerHTML = '';
        // Sepete eklenen ürünleri listele ve arttırma/azaltma
        cartItems.forEach(item => {
            const listItem = document.createElement('li');
            listItem.textContent = `${item.name} - $${item.price} x `;

            // Sayısal giriş kutusu (numericupdown)
            const quantityInput = document.createElement('input');
            quantityInput.type = 'number';
            quantityInput.value = item.quantity;
            quantityInput.min = 1;
            quantityInput.classList.add('cart-item-quantity'); // Class ekleyin
            quantityInput.addEventListener('change', function () {
                item.quantity = parseInt(quantityInput.value);
                updateCartPanel();
            });
            // Çöp tenekesi ikonu
            const trashIcon = document.createElement('i');
            trashIcon.classList.add('fa', 'fa-trash', 'col-1');
            trashIcon.addEventListener('click', function () {
                removeItemFromCart(item.id);
            });
            function removeItemFromCart(productId) {
                const index = cartItems.findIndex(item => item.id === productId);
                if (index !== -1) {
                    cartItems.splice(index, 1);
                    updateCartPanel();
                }
            }
            // Liste öğesine düğmeleri ve çöp tenekesi ikonunu ekle
            listItem.appendChild(quantityInput);
            listItem.appendChild(trashIcon);
            // Liste öğesini sepete ekle
            cartItemsList.appendChild(listItem);
        });
        // Sepetin toplam fiyatını hesapla ve göster
        const totalPrice = cartItems.reduce((total, item) => total + parseFloat(item.price) * item.quantity, 0);
        cartTotal.textContent = `Toplam: $${totalPrice.toFixed(2)}`;
        // Boş sepet mesajını göster veya gizle
        if (cartItems.length === 0) {
            emptyCartMessage.style.display = 'block';
        } else {
            emptyCartMessage.style.display = 'none';
        }
        // Sepet panelini göster
        cartPanel.classList.add('show');
        // Numaric giriş kutularını kontrol et ve azaltma düğmesine basıldığında sil
        const quantityInputs = document.querySelectorAll('.cart-item-quantity');
        quantityInputs.forEach(input => {
            input.addEventListener('change', function () {
                const quantity = parseInt(input.value);
                if (quantity === 1) {
                    const productId = input.parentElement.querySelector('.fa-trash').getAttribute('data-product-id');
                    removeItemFromCart(productId);
                }
            });
        });
    }
    // Sepeti temizleme butonuna tıklandığında çalışacak fonksiyon
    clearCartBtn.addEventListener('click', function () {
        // Sepeti temizle
        clearCart();
        // birkaç saniye bekledikten sonra, sepeti kapat
        setTimeout(function () {
            cartPanel.classList.remove('show');
        }, 400); // 1000 milisaniye = 1 saniye
    });
    // Sepete ekleme butonlarına tıklandığında çalışacak fonksiyon
    document.addEventListener('click', function (event) {
        if (event.target.matches('.add-to-cart')) {
            var productId = event.target.getAttribute('data-product-id');
            var productName = event.target.getAttribute('data-product-name');
            var productPrice = event.target.getAttribute('data-product-price');
            // Kullanıcı giriş yapmış mı kontrol et
            var isLoggedIn = document.body.getAttribute('data-logged-in') === 'True';
            if (!isLoggedIn) {
                alert('Öncelikle giriş yapınız.');
                window.location.href = '/Home/IndexLogin';
                return;
            }
            // Aşağıda ki kod kısmı, ürün sepete eklenme işlevi için olan kodlardır.
            const existingItem = cartItems.find(item => item.id === productId);
            if (existingItem) {
                alert('Bu ürün zaten sepete eklenmiş!');
            } else {
                cartItems.push({ id: productId, name: productName, price: productPrice, quantity: 1 });
                updateCartPanel();
            }
        }
    });
});
