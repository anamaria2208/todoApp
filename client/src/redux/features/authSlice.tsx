import {  createSlice } from '@reduxjs/toolkit'


export interface AuthState {
  isLoggedIn : boolean,
}

const initialState: AuthState = {
  isLoggedIn : false,
}

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    loginSuccess: (state) => {
      state.isLoggedIn = true
    },
    logoutSuccess : (state) => {
      state.isLoggedIn = false
    }
  }
})


export const {loginSuccess, logoutSuccess} = authSlice.actions
export default authSlice.reducer