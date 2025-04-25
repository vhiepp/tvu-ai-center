'use client';

import React, { useState, useEffect, useMemo, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import { apiClient } from '@/apis/api-client';
import { createCategoryApi } from '@/apis/admin/category';
import { useRouter } from 'next/navigation';
import Editor from '@/components/Editor';

interface DropdownItem {
    name: string;
    code: string;
}

const RoleAdminCreate = () => {
    const [loading1, setLoading1] = useState(false);
    // const [permissions, setPermissions] = useState([]);
    const [switchValue, setSwitchValue] = useState({});
    const [categoryViName, setCategoryViName] = useState('');
    const [categoryEnName, setCategoryEnName] = useState('');
    const [descriptionVi, setDescriptionVi] = useState('');
    const [descriptionEn, setDescriptionEn] = useState('');

    const [contentVi, setContentVi] = useState('');
    const [contentEn, setContentEn] = useState('');

    const categoryViNameRef = useRef(null);
    const categoryEnNameRef = useRef(null);
    const router = useRouter();

    // const getPermissions = async () => {
    //     const response = await apiClient.get(getAdminPermissionsApi);
    //     const pers = response.data.data;
    //     pers.forEach((per) => {
    //         setSwitchValue((prev) => ({ ...prev, [per.id]: false }));
    //     });
    //     setPermissions(pers);
    // };

    const changeSwitchValue = (key: number, value: boolean) => {
        setSwitchValue((prev) => ({ ...prev, [key]: value }));
    };

    const handleSaveContent = (content: any, language: string) => {
        console.log({ content });
    };

    const saveRole = async (exit = false) => {
        if (!categoryViName) {
            categoryViNameRef.current.focus();
            return;
        }
        if (!categoryEnName) {
            categoryEnNameRef.current.focus();
            return;
        }
        // const pers = Object.keys(switchValue)
        //     .filter((key) => switchValue[key])
        //     .map(Number);
        // if (pers.length == 0) {
        //     alert('Vui lòng chọn ít nhất một quyền');
        //     return;
        // }
        setLoading1(true);
        const data = {
            categoryContents: [
                {
                    language: 'vi',
                    name: categoryViName,
                    description: descriptionVi
                },
                {
                    language: 'en',
                    name: categoryEnName,
                    description: descriptionEn
                }
            ]
        };

        const response = await apiClient.post(createCategoryApi, data);

        setLoading1(false);

        if (response.status == 200) {
            if (exit) {
                router.push('/categories');
            } else {
                setCategoryViName('');
                setCategoryEnName('');
                setDescriptionVi('');
                setDescriptionEn('');
                categoryViNameRef.current.focus();
            }
        } else {
            alert('Có lỗi xảy ra');
        }
    };

    useEffect(() => {
        // getPermissions();
    }, []);

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <h5>Thêm tin tức</h5>
                    <div className="p-fluid formgrid grid">
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Tiêu đề <span className="p-error">(*)</span>
                            </label>
                            <InputText ref={categoryViNameRef} placeholder="Tiêu đề tiếng việt" id="role_name" value={categoryViName} onChange={(e) => setCategoryViName(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Title <span className="p-error">(*)</span>
                            </label>
                            <InputText ref={categoryEnNameRef} placeholder="Tiêu đề tiếng anh" id="role_name" value={categoryEnName} onChange={(e) => setCategoryEnName(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="address" className="text-lg">
                                Nội dung{' '}
                                {/* <span className="text-sm">
                                    <i>(không bắt buộc)</i>
                                </span> */}
                            </label>
                            {/* <InputTextarea id="description" value={descriptionVi} onChange={(e) => setDescriptionVi(e.target.value)} rows={2} /> */}
                            <Editor onChange={handleSaveContent} data={contentVi} />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="address" className="text-lg">
                                Content{' '}
                                {/* <span className="text-sm">
                                    <i>(không bắt buộc)</i>
                                </span> */}
                            </label>
                            <Editor onChange={handleSaveContent} data={contentEn} />
                        </div>

                        {/* <div className="field col-12">
                            <label className="text-lg">Các quyền hạn</label>
                        </div>
                        <div className="field col-12">
                            <div className="grid">
                                {permissions.map((permission) => {
                                    return (
                                        <div className="field col-12 md:col-6 lg:col-4" key={permission.id} title={permission.description}>
                                            <div className="flex align-items-center gap-2 pb-2">
                                                <InputSwitch checked={switchValue[permission.id]} onChange={(e) => changeSwitchValue(permission.id, e.value ?? false)} />
                                                <label className="text-lg">{permission.name}</label>
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div> */}

                        <div className="field col-12">
                            <div className="grid mt-4 justify-content-end align-items-center gap-2">
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu và thêm mới" icon="pi pi-save" className="p-button-info" loading={loading1} onClick={() => saveRole()} />
                                </div>
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu và thoát" icon="pi pi-save" className="p-button-success" loading={loading1} onClick={() => saveRole(true)} />
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
