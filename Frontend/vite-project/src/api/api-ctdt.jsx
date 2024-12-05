import API_BASE_URL from "./base-url";
import axios from 'axios';
import { getAccessToken } from "../utils/storage";

const API_CTDT = `${API_BASE_URL}/api/ctdt`;

export const getAllCTDTs = async () => {
  try {
    const response = await axios.get(API_CTDT, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addCTDT = async (newData) => {
  try {
    const response = await axios.post(API_CTDT, newData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updateCTDT = async (id, updatedData) => {
  try {
    console.log("id & updatedData", id, updatedData);
    const response = await axios.put(`${API_CTDT}/${id}`, updatedData, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deleteCTDT = async (id) => {
  try {
    const response = await axios.delete(`${API_CTDT}/${id}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getHocPhansByCTDTId = async (cTDTId) => {
  try {
    const response = await axios.get(`${API_CTDT}/${cTDTId}/view-hocphans`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const addHocPhansToCTDT = async (cTDTId, hocPhanIdsList) => {
  try {
    const response = await axios.post(`${API_CTDT}/${cTDTId}/add-hocphans`, hocPhanIdsList, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const removeHocPhanFromCTDT = async (cTDTId, hocPhanId) => {
  try {
    const response = await axios.delete(`${API_CTDT}/${cTDTId}/remove-hocphan/${hocPhanId}`, {
      headers: { Authorization: getAccessToken() }
    });
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}
