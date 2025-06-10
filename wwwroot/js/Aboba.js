const CATEGORIES_URL = "/CategoryPage/GetCategories";
const PRODUCTS_PAGE_URL = "Views/ProductsPage/ProductsPage.cshtml";

// Функция для отправки запроса на получение категорий
async function GetCategories(furl) {
    try {
        const response = await fetch(furl);
        if (!response.ok) {
            throw new Error(`Fetch Categories Err: ${response.status}`);
        }
        const data = await response.json();
        return data; // Возвращаем data без проверки структуры здесь.
    } catch (error) {
        console.error(`GetCategories Err: ${error}`);
        return null; // Возвращаем null, если произошла ошибка
    }
}

// Функция для загрузки изображения
async function GetImg(furl) {
    furl = furl ?? "../images/default-image.svg";
    try {
        const response = await fetch(furl);
        if (!response.ok) {
            throw new Error(`Fetch Image Err: ${response.status}`);
        }
        const blob = await response.blob();
        return URL.createObjectURL(blob);
    } catch (error) {
        console.error("Err: Error image loading:", error);
        return "../images/default-image.svg";
    }
}

// Функция для создания HTML-элемента категории
async function createHTMLcategory(category) {
    if (!category || !category.name || !category.pictureUrl) {
        if (!category) console.error("Err: Object is null or undefined");
        if (!category.name) console.error("Err: object doesn't have name");
        if (!category.pictureUrl) console.error("Err: object doesn't have pictureUrl");
        return false;
    }

    try {
        const categoriesZone = document.getElementById('category-container');
        if (!categoriesZone) {
            console.error("Err: category-container not found");
            return false;
        }

        const categoryLink = document.createElement('a');
        categoryLink.href = `${PRODUCTS_PAGE_URL}?categoryName=${encodeURIComponent(category.name)}`; // Всегда переходим на страницу товаров

        categoryLink.classList.add('category-card_N23');

        const categoryName = document.createElement('div');
        categoryName.textContent = category.name;
        categoryName.classList.add('category-name_N23');
        categoryLink.appendChild(categoryName);

        const categoryImageContainer = document.createElement('div');
        categoryImageContainer.classList.add('category-image-container_N23');

        const categoryImage = document.createElement('img');
        categoryImage.classList.add('category-image_N23');
        const CatImg = await GetImg(category.pictureUrl);
        categoryImage.src = CatImg;
        categoryImage.alt = category.name;

        categoryImageContainer.appendChild(categoryImage);
        categoryLink.appendChild(categoryImageContainer);
        categoriesZone.appendChild(categoryLink);
        return true;
    } catch (error) {
        console.error("Err: create category error", error);
        return false;
    }
}

// Загрузка категорий
async function loadCategories(furl) {
    try {
        if (!furl) {
            throw new Error("Err loadCategories: not found a furl");
        }

        const data = await GetCategories(furl);

        if (!data) {
            console.error("Err loadCategories: Data is null or undefined");
            return;
        }

         if (data.categories && Array.isArray(data.categories)) {
            // Если есть массив categories, отображаем категории
            const categoriesZone = document.getElementById("category-container");
            if (!categoriesZone) {
                console.error("Err: category-container not found");
                return;
            }

         else if (data.products && Array.isArray(data.products)) {
                 const response = await fetch("ProductsPage/ProductsPage");
                 // Если есть массив products, переходим на страницу товаров
                 //window.location.href = 'Views/ProductsPage/ProductsPage.cshtml'; // Сохраняем параметр categoryName, если он был

             }


            categoriesZone.innerHTML = ""; // Очищаем содержимое

            for (const category of data.categories) {
                await createHTMLcategory(category);
            }
        } else {
            console.warn("loadCategories: No products or categories found in data.");
            //Вставить логику для обработки запроса, если нет ни products, ни categories
        }
    } catch (error) {
        console.error("Err loadCategories", error);
    }
}

// Функция для восстановления позиции прокрутки
function restoreScrollPosition() {
    const scrollY = sessionStorage.getItem('scrollPosition');
    if (scrollY !== null) window.scrollTo({ top: parseInt(scrollY), behavior: 'auto' });
}

// Функция для сохранения позиции прокрутки
function saveScrollPosition() {
    sessionStorage.setItem('scrollPosition', window.pageYOffset.toString());
}

// Подписываемся на событие прокрутки окна
window.onbeforeunload = function () {
    saveScrollPosition();
};

// Восстановление положения прокрутки при загрузке страницы
restoreScrollPosition();

// Инициализация страницы при загрузке
document.addEventListener("DOMContentLoaded", function () {
    let furl = CATEGORIES_URL;

    // Извлекаем GET-параметры из текущего URL
    const params = new URLSearchParams(window.location.search);
    const categoryName = params.get("categoryName");

    if (categoryName) {
        furl += `?categoryName=${encodeURIComponent(categoryName)}`;
    }

    (async () => {
        await loadCategories(furl);
    })();
});
