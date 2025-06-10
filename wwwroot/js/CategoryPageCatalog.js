async function GetCategories(furl) {
    try {
        const response = await fetch(furl);
        console.log(1234567890);
        if (!response.ok) {
            throw new Error(`Fetch Categories Err: ${response.status}`);
        }
        const data = await response.json();
        if (data) {
            if (isJsonObject(data)) {
                return data;
            }
            else {
                return response.status
            }
        } else {
            console.error("GetCategories Err: Invalid JSON format. Missing 'categories' array.");
            return [];
        }
    } catch (error) {
        console.error(`GetCategories Err: ${error}`);
        return [];
    }
}

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
        category.href = '#';

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

        category.addEventListener("click", async (event) => {
            event.preventDefault();
            const categoryName = object.name;
            const newFurl = `CategoryPage/GetCategories?categoryName=${categoryName}`; // Формируем URL
            console.log(await GetCategories(newFurl))
            await loadCategories(newFurl);
        });

    } catch (error) {
        console.error("Err: create category error", error);
    }
}

async function loadCategories(furl) {
    try {
        if (!furl) {
            throw new Error('Err loadCategories: not found a furl');
        }
        const categories = await GetCategories(furl);
        const categoriesZone = document.getElementById('category-container'); 
        if (!categoriesZone) {
            console.error("Err: category-container not found");
            return;
        }

        while (categoriesZone.firstChild) {
             categoriesZone.removeChild(categoriesZone.firstChild);
        }

        for (const category of categories) {
            await createHTMLcategory(category);
        }
    }
    catch (error) {
        console.error("Err loadCategories", error);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const furl = "/CategoryPage/GetCategories";
    console.log(123);
    (async () => {
        await loadCategories(furl);
    })();
});
