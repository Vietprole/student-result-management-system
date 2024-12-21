import { jwtDecode } from "jwt-decode"

export const getAccessToken = () => {
  const token = sessionStorage.getItem('accessToken')
  return token ? `Bearer ${token}` : ''
}
export const saveAccessToken = (token) => {
  console.log(">>Save token:", token);
  sessionStorage.setItem('accessToken', token)
}
export const tokenBear = (token) => {
  return 'Bearer ' + token
}

export const getFullname = () => {
  const result = sessionStorage.getItem('accessToken').toString();
  const decodedToken = jwtDecode(result);
  const fullname = decodedToken.fullname;
  return fullname;
}
export const getRole = () => {
  const result = sessionStorage.getItem('accessToken')
  const decodedToken = jwtDecode(result);
  const role = decodedToken.role;
  console.log(">>Role:", role);
  return role;
}

export const getGiangVienId = () => {
  const result = sessionStorage.getItem('accessToken')
  const decodedToken = jwtDecode(result);
  const giangVienId = decodedToken.giangVienId;
  return parseInt(giangVienId);
}
