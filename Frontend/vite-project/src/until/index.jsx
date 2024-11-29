export const getAccessToken = () => {
  const token = sessionStorage.getItem('accesstoken')
  return token ? `Bearer ${token}` : ''
}
export const saveAccessToken = (token) => {
  sessionStorage.setItem('accesstoken', token)
}
export const tokenBear = (token) => {
  return 'Bearer ' + token
}

export const getProfile = () => {
  const result = sessionStorage.getItem('profile')
  return result ? JSON.parse(result) : null
}

export const saveProfile = (profile) => {
  sessionStorage.setItem('profile', JSON.stringify(profile))
}

export const clearProfile = () => {
  sessionStorage.removeItem('profile')
}
