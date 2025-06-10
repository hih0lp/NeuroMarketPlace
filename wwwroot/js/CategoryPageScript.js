// Функция для отправки запроса на получение категорий
async function GetCategories(furl) {
    try {
        const response = await fetch(furl);
        if (!response.ok) {
            throw new Error(`Fetch Categories Err: ${response.status}`);
        }
        const data = await response.json();
        if (data) {
            return data;
        } else {
            console.error("GetCategories Err: Invalid JSON format. Missing 'categories' array.");
            return [];
        }
    } catch (error) {
        console.error(`GetCategories Err: ${error}`);
        return [];
    }
}

// Функция для загрузки изображения
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

// Функция для создания HTML-элемента категории
async function createHTMLcategory(object) {
    if (!object || !object.name || !object.pictureUrl) {
        if (!object.name) {
            throw new Error("Err: object doesn't have name");
        }
        if (!object.pictureUrl) {
            throw new Error("Err: object doesn't have pictureUrl");
        }
    }

    try {
        const categoriesZone = document.getElementById('category-container');
        if (!categoriesZone) {
            console.error("Err: category-container not found");
            return;
        }

        const category = document.createElement('a');
        category.classList.add('category-card_N23');
        // Изменяем href так, чтобы перенаправление происходило на тот же файл,
        // передавая выбранную категорию через GET-параметры
        category.href = `${window.location.pathname}?categoryName=${encodeURIComponent(object.name)}`;

        const categoryName = document.createElement('div');
        categoryName.textContent = object.name;
        categoryName.classList.add('category-name_N23');
        category.appendChild(categoryName);

        const categoryImageContainer = document.createElement('div');
        categoryImageContainer.classList.add('category-image-container_N23');

        const categoryImage = document.createElement('img');
        categoryImage.classList.add('category-image_N23');
        const CatImg = await GetImg(object.pictureUrl);
        categoryImage.src = CatImg;
        categoryImage.alt = object.name;

        categoryImageContainer.appendChild(categoryImage);
        category.appendChild(categoryImageContainer);
        categoriesZone.appendChild(category);
    } catch (error) {
        console.error("Err: create category error", error);
    }
}

// Загрузка категорий
async function loadCategories(furl) {
    try {
        if (!furl) {
            throw new Error('Err loadCategories: not found a furl');
        }
        const categoriesJSON = await GetCategories(furl);

        if (categoriesJSON && categoriesJSON.categories) {
            const categoriesZone = document.getElementById('category-container');
            if (!categoriesZone) {
                console.error("Err: category-container not found");
                return;
            }

            while (categoriesZone.firstChild) {
                categoriesZone.removeChild(categoriesZone.firstChild);
            }

            for (const category of categoriesJSON.categories) {
                await createHTMLcategory(category);
            }
        }
        else if (categoriesJSON && categoriesJSON.products) {
            sessionStorage.setItem('productsData', JSON.stringify(categoriesJSON))
            window.location.href = "ProductsPage/ProductsPage";
        }
        else {
            console.warn("API response does not contain categories or products.");
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
    let furl = "/CategoryPage/GetCategories";

    // Извлекаем GET-параметры из текущего URL
    const params = new URLSearchParams(window.location.search);
    const categoryName = params.get('categoryName');

    if (categoryName) {
        // Передаем имя категории прямо в запрос
        furl += `?categoryName=${encodeURIComponent(categoryName)}`;
    }

    console.log(123);
    (async () => {
        await loadCategories(furl);
        console.log(GetCategories(furl));
    })();
});
