import API_BASE_URL from "./base-url";
import axios from 'axios';

const API_PLO = `${API_BASE_URL}/api/plo`;

export const getPLOsByLopHocPhanId = async (lopHocPhanId) => {
  console.log("lopHocPhanId: ", lopHocPhanId);
  try {
    const response = await axios.get(`${API_PLO}?lopHocPhanId=${lopHocPhanId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getAllPLOs = async () => {
  try {
    const response = await axios.get(API_PLO);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const getPLOById = async (id) => {
  try {
    const response = await axios.get(`${API_PLO}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const addPLO = async (newData) => {
  try {
    const response = await axios.post(API_PLO, newData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const updatePLO = async (id, updatedData) => {
  try {
    const response = await axios.put(`${API_PLO}/${id}`, updatedData);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const deletePLO = async (id) => {
  try {
    const response = await axios.delete(`${API_PLO}/${id}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
};

export const getCLOsByPLOId = async (ploId) => {
  console.log("ploId: ", ploId);
  try {
    const response = await axios.get(`${API_PLO}/${ploId}/view-clos`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const addCLOsToPLO = async (ploId, cloIdsList) => {
  try {
    const response = await axios.post(`${API_PLO}/${ploId}/add-clos`, cloIdsList);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const updateCLOsToPLO = async (ploId, cloIdsList) => {
  try {
    const response = await axios.put(`${API_PLO}/${ploId}/update-clos`, cloIdsList);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

export const removeCLOsFromPLO = async (ploId, cloId) => {
  try {
    const response = await axios.delete(`${API_PLO}/${ploId}/remove-clo/${cloId}`);
    return response.data;
  } catch (error) {
    console.log("error message: ", error.message);
  }
}

