import { apiClient } from '@/apis/api-client';

export function getToken() {
    return localStorage.getItem('token') || '';
}

export function setToken(token: string) {
    localStorage.setItem('token', token);
}

export const fetcher = (url: string) => apiClient.get(url).then((res) => res.data.data);

export function base64ToFile(base64String, filename) {
    const arr = base64String.split(',');
    const mime = arr[0].match(/:(.*?);/)[1];
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
}