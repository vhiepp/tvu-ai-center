'use client';

import React, { useState, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { apiClient } from '@/apis/api-client';
import { useRouter } from 'next/navigation';
import dynamic from 'next/dynamic';
import { createPartnerApi } from '@/apis/partner';

const ImagePicker = dynamic(() => import('@/components/forms/ImagePicker'), { ssr: false });

interface DropdownItem {
    name: string;
    code: string;
}

interface InputValue {
    name: string;
    code: number;
}

const statusValues: Array<InputValue> = [
    { name: 'Hiển thị', code: 1 },
    { name: 'Ẩn', code: 2 }
];

const RoleAdminCreate = () => {
    const [loading1, setLoading1] = useState(false);

    const [avatar, setAvatar] = useState<any>(null);
    const [fullName, setFullName] = useState('');

    const fullNameRef = useRef(null);

    const router = useRouter();

    function base64ToFile(base64String, filename) {
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

    const save = async (exit = false) => {
        if (!fullName) {
            fullNameRef.current.focus();
            return;
        }
        if (!avatar) {
            alert('Vui lòng chọn ảnh logo');
            return;
        }
        setLoading1(true);
        const formData = new FormData();
        const file = base64ToFile(avatar, 'upload.png');
        formData.append('Logo', file);
        formData.append('Name', fullName);

        const response = await apiClient.post(createPartnerApi, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });

        setLoading1(false);
        if (response.status == 200) {
            if (exit) {
                router.push('/partner');
            } else {
                setAvatar(null);
                setFullName('');
            }
        } else {
            alert('Có lỗi xảy ra');
        }
    };

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <h5>Thêm đối tác</h5>
                    <div className="p-fluid formgrid grid">
                        <div className="field col-12">
                            <label htmlFor="role_name" className="text-lg">
                                Tên <span className="p-error">(*)</span>
                            </label>
                            <InputText ref={fullNameRef} placeholder="Tên đối tác" value={fullName} onChange={(e) => setFullName(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="password" className="text-lg">
                                Logo <span className="p-error">(*)</span>
                            </label>
                            <ImagePicker setImageSrc={setAvatar} width="300px" height="200px" objectFit="cover" />
                        </div>

                        <div className="field col-12">
                            <div className="grid mt-4 justify-content-end align-items-center gap-2">
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu và thêm mới" icon="pi pi-save" className="p-button-info" loading={loading1} onClick={() => save()} />
                                </div>
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu và thoát" icon="pi pi-save" className="p-button-success" loading={loading1} onClick={() => save(true)} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RoleAdminCreate;
