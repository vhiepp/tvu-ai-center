export const createMemberApi = '/members';
export const getMemberApi = '/members';
export const getMemberByIdApi = (id: string) => `/members/${id}`;
export const updateMemberApi = (id: string) => `/members/${id}`;
export const deleteMemberApi = (id: string) => `/members/${id}`;
export const deleteMemberMultipleApi = '/members/multiple';