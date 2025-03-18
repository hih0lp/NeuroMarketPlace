//async function GetCategories() {
//    const furl = "CategoryPage/GetCategories";
//    try {
//        const response = await fetch(furl);
//        if (!response.ok) {
//            throw new Error(`Fetch Categories Err: ${response.status}`);
//        }
//        const objects = await response.json();
//        console.log(objects)
//        return objects;
//    } catch (error) {
//        console.error(`GetCategories Err: ${error}`);
//        return [];
//    }
//}


//async function GetImg(furl) {
//    if (!furl) {
//        console.error("Err: furl is missing");
//        return null;
//    }

//    try {
//        const response = await fetch(furl);
//        if (!response.ok) {
//            throw new Error(`Fetch Image Err: ${response.status}`);
//        }
//        const blob = await response.blob();
//        const imageUrlObject = URL.createObjectURL(blob);
//        return imageUrlObject;
//    } catch (error) {
//        console.error("Err: Error image loading:", error);
//        return "../images/default-image.svg";
//    }
//}

//async function createHTMLcategory(object) {
//    if (!object || !object.name || !object.pictureUrl) {
//        console.error("Err: Invalid category object");
//        return;
//    }

//    try {
//        const categoriesZone = document.getElementById('category-container');
//        if (!categoriesZone) {
//            console.error("Err: category-container not found");
//            return;
//        }
//        const origCategory = document.getElementById('example');
//        if (!origCategory) {
//            console.error("Err: example category not found");
//            return;
//        }

//        const CatName = object.name;
//        const CatImg = await GetImg(object.pictureUrl);

//        const newCategory = origCategory.cloneNode(true);
//        newCategory.id = `category-${CatName.toLowerCase().replace(/\s+/g, '-')}`;

//        const catNameElement = newCategory.querySelector(".ded");
//        if (catNameElement) {
//            catNameElement.innerHTML = CatName.replace(/\s+/g, "<br />");
//        }

//        const catImageElement = newCategory.querySelector("img");
//        if (catImageElement && CatImg) {
//            catImageElement.src = CatImg;
//            catImageElement.onload = () => {
//                URL.revokeObjectURL(CatImg);
//            };
//        }

//        categoriesZone.appendChild(newCategory);

//    } catch (error) {
//        console.error("Error creating category HTML:", error);
//    }
//}

//async function loadCategories() {
//    const categories = await GetCategories();
//    if (categories && Array.isArray(categories)) {
//        for (const category of categories) {
//            await createHTMLcategory(category);
//        }
//    }
//}

//document.addEventListener("DOMContentLoaded", loadCategories);

async function GetCategories() {
    const furl = "CategoryPage/GetCategories";
    try {
        const response = await fetch(furl);
        if (!response.ok) {
            throw new Error(`Fetch Categories Err: ${response.status}`);
        }
        const data = await response.json();
        if (data && Array.isArray(data.categories)) {
            return data.categories;
        } else {
            console.error("GetCategories Err: Invalid JSON format.  Missing 'categories' array.");
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
        console.error("Err: Invalid category object");
        return;
    }

    try {
        const categoriesZone = document.getElementById('category-container');
        if (!categoriesZone) {
            console.error("Err: category-container not found");
            return;
        }
        const origCategory = document.getElementById('example');
        if (!origCategory) {
            console.error("Err: example category not found");
            return;
        }

        const CatName = object.name;
        const CatImg = await GetImg(object.pictureUrl);

        const newCategory = origCategory.cloneNode(true);
        newCategory.id = `category-${CatName.toLowerCase().replace(/\s+/g, '-')}`;

        const catNameElement = newCategory.querySelector(".ded");
        if (catNameElement) {
            catNameElement.innerHTML = CatName.replace(/\s+/g, "<br />");
        }

        const catImageElement = newCategory.querySelector("img");
        if (catImageElement && CatImg) {
            catImageElement.src = CatImg;
            catImageElement.onload = () => {
                URL.revokeObjectURL(CatImg);
            };
        }

        categoriesZone.appendChild(newCategory);

    } catch (error) {
        console.error("Error creating category HTML:", error);
    }
}

async function loadCategories() {
    const categories = await GetCategories();
    if (categories && Array.isArray(categories)) {
        for (const category of categories) {
            await createHTMLcategory(category);
        }
    }
}

document.addEventListener("DOMContentLoaded", loadCategories);