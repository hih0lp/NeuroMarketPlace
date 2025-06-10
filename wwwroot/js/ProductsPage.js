const data = JSON.parse(sessionStorage.getItem('productsData')).products

async function GetImg(furl) {
    if (!furl) {
        console.error("Err: furl is missing");
        return null;
    }

    try {
        const response = await fetch(furl);
        if (!response.ok) {
            throw new Error(`Fetch Image Err: ${response.status}`);
        }
        const blob = await response.blob();
        const imageUrlObject = URL.createObjectURL(blob);
        return imageUrlObject;
    } catch (error) {
        console.error("Err: Error image loading:", error);
        return "../images/default-image.svg";
    }
}

async function createHTMLProductCard(product) {
    if (!product || !product.name || !product.pictureurl || !product.price) {
        console.error("Ошибка: Неверный объект продукта.");
        if (!product.name) console.error("Продукту не хватает имени.");
        if (!product.pictureurl) console.error("Продукту не хватает URL изображения.");
        if (!product.price) console.error("Продукту не хватает цены.");
        return;
    }

    try {
        const productsContainer = document.getElementById('products-grid');
        if (!productsContainer) {
            console.error("Ошибка: Контейнер продуктов 'products-container' не найден.");
            return;
        }

        const productCard = document.createElement('a');
        productCard.href = product.url ? product.url : "#"; 
        productCard.classList.add('product-card');

        const productImage = document.createElement('img');
        const productImgSrc = await GetImg(product.pictureurl); 
        productImage.src = productImgSrc;
        productImage.alt = product.name;
        productImage.classList.add('product-image');
        productCard.appendChild(productImage);

        const productPrice = document.createElement('div');
        productPrice.classList.add('product-price');
        productPrice.textContent = `${product.price.toLocaleString()} ₽`; 
        productCard.appendChild(productPrice);

        const productTitle = document.createElement('h3');
        productTitle.classList.add('product-title');
        productTitle.textContent = product.name;
        productCard.appendChild(productTitle);

        const productRating = document.createElement('div');
        productRating.classList.add('product-rating');
        productRating.textContent = product.reviewrating ? product.reviewrating : 'Нет отзывов'; 
        productCard.appendChild(productRating);

        const productActions = document.createElement('div');
        productActions.classList.add('product-actions');

        const addToCartButton = document.createElement('button');
        addToCartButton.classList.add('add-to-cart');
        addToCartButton.innerHTML = '<strong> Подробнее</strong><img class="filter-photo-card" src="../images/детдом.svg" alt="">';
        productActions.appendChild(addToCartButton);

        const favoriteButton = document.createElement('button');
        favoriteButton.classList.add('favorite-btn');
        favoriteButton.innerHTML = '<i class="fa fa-heart"></i>';
        productActions.appendChild(favoriteButton);

        productCard.appendChild(productActions);

        productsContainer.appendChild(productCard);

    } catch (error) {
        console.error("Ошибка: Создание карточки продукта", error);
    }
}

document.addEventListener('DOMContentLoaded', function () {
    if (data && Array.isArray(data)) {
        data.forEach(async product => {
            await createHTMLProductCard(product);
        });
    } else {
        console.warn("No products data to display.");
    }
});
