'use client';

import React, { useState, useEffect, useMemo, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import { apiClient } from '@/apis/api-client';
import { useRouter } from 'next/navigation';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import dynamic from 'next/dynamic';
import { getPageContentApi, updatePageContentApi } from '@/apis/page-content-api';
import { Toast } from 'primereact/toast';

const Editor = dynamic(() => import('@/components/Editor'), { ssr: false });
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
    const [loadDataSuccess, setloadDataSuccess] = useState(false);
    const [enable, setEnable] = useState(false);

    const [contentVi, setContentVi] = useState('');
    const [contentEn, setContentEn] = useState('');

    const toastRef = useRef<Toast>(null);

    const saveData = async () => {
        // console.log({ contentVi, contentEn });
        setLoading1(true);
        const data = [
            {
                content: JSON.stringify(contentVi),
                language: 'vi'
            },
            {
                content: JSON.stringify(contentEn),
                language: 'en'
            }
        ];
        const response = await apiClient.put(updatePageContentApi('about'), data);
        setLoading1(false);
        if (response.status == 200) {
            showSuccess();
        } else {
            showError();
        }
    };

    console.log('contentVi', contentVi);

    const getData = async () => {
        try {
            const response = await apiClient.get(getPageContentApi('about'));
            // console.log(response.data);
            const data = response.data.data;
            const viItem = data.find((item) => item.language === 'vi');
            const enItem = data.find((item) => item.language === 'en');
            // console.log('viItem', viItem);
            if (viItem) {
                setContentVi(JSON.parse(viItem.content));
            }
            if (enItem) {
                setContentEn(JSON.parse(enItem.content));
            }

            setloadDataSuccess(true);
        } catch (error) {
            console.error('error', error);
        }
    };

    const showSuccess = () => {
        toastRef.current?.show({
            severity: 'success',
            summary: 'Success',
            detail: 'Lưu thành công',
            life: 3000
        });
    };

    const showError = () => {
        toastRef.current?.show({
            severity: 'error',
            summary: 'Error',
            detail: 'Có lôi xảy ra',
            life: 3000
        });
    };

    useEffect(() => {
        getData();
    }, []);

    return (
        <div className="grid">
            <Toast ref={toastRef} />
            <div className="col-12">
                <div className="card">
                    <div className="p-fluid formgrid grid">
                        <div className="field col-12 flex">
                            <label htmlFor="name" className="text-lg mr-2">
                                Chỉnh sửa
                            </label>
                            <InputSwitch checked={enable} onChange={(e) => setEnable(e.value ?? false)} />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="address" className="text-lg">
                                Nội dung{' '}
                            </label>
                            {loadDataSuccess && <Editor onChange={(content) => setContentVi(content)} data={contentVi} readOnly={!enable} />}
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="address" className="text-lg">
                                Content{' '}
                            </label>
                            {loadDataSuccess && <Editor onChange={(content) => setContentEn(content)} data={contentEn} readOnly={!enable} />}
                        </div>

                        <div className="field col-12">
                            <div className="grid mt-4 justify-content-end align-items-center gap-2">
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu" icon="pi pi-save" className="p-button-success" loading={loading1} onClick={() => saveData()} />
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
