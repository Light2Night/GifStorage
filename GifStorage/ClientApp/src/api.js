import axios from 'axios';

export const invalidImageUrl = "images/invalid.png";

export const getGifsAsync = async () => {
    const response = await axios.get("api/gif/get");
    return response.data;
}

export const createCategoryAsync = async (form) => {
    return await axios.post("api/gif/create", form, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });
}

export const deleteCategoryAsync = async (id) => {
    return await axios.delete(`api/gif/delete/${id}`);
}