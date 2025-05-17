export const createNewsApi = '/news';
export const getNewsListApi = '/news';
export const getNewsDetailApi = (id: string) => `/news/detail/${id}`;
export const updateNewsApi = (id: string) => `/news/${id}`;
export const deleteNewsApi = (id: string) => `/news/${id}`;
export const deleteNewsMultipleApi = '/news/multiple';