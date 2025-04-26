'use client';

import React, { useState, useEffect, useMemo, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import { apiClient } from '@/apis/api-client';
import { createCategoryApi, getCategoriesApi } from '@/apis/admin/category';
import { useRouter } from 'next/navigation';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import dynamic from 'next/dynamic';

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
    // const [permissions, setPermissions] = useState([]);
    const [switchValue, setSwitchValue] = useState({});
    const [categoryViName, setCategoryViName] = useState('');
    const [categoryEnName, setCategoryEnName] = useState('');
    const [descriptionVi, setDescriptionVi] = useState('');
    const [descriptionEn, setDescriptionEn] = useState('');
    const [multiselectValue, setMultiselectValue] = useState(null);
    const [multiselectValues, setMultiselectValues] = useState<DropdownItem[]>([]);
    const [selectStatusValue, setSelectStatusValue] = useState(statusValues[0]);

    // const multiselectValues = [
    //     { name: 'Australia', code: 'AU' },
    //     { name: 'Brazil', code: 'BR' },
    //     { name: 'China', code: 'CN' },
    //     { name: 'Egypt', code: 'EG' },
    //     { name: 'France', code: 'FR' },
    //     { name: 'Germany', code: 'DE' },
    //     { name: 'India', code: 'IN' },
    //     { name: 'Japan', code: 'JP' },
    //     { name: 'Spain', code: 'ES' },
    //     { name: 'United States', code: 'US' }
    // ];

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
        // if (!categoryViName) {
        //     categoryViNameRef.current.focus();
        //     return;
        // }
        // if (!categoryEnName) {
        //     categoryEnNameRef.current.focus();
        //     return;
        // }
        // const pers = Object.keys(switchValue)
        //     .filter((key) => switchValue[key])
        //     .map(Number);
        // if (pers.length == 0) {
        //     alert('Vui lòng chọn ít nhất một quyền');
        //     return;
        // }
        // setLoading1(true);
        // const data = {
        //     categoryContents: [
        //         {
        //             language: 'vi',
        //             name: categoryViName,
        //             description: descriptionVi
        //         },
        //         {
        //             language: 'en',
        //             name: categoryEnName,
        //             description: descriptionEn
        //         }
        //     ]
        // };
        // const response = await apiClient.post(createCategoryApi, data);
        // setLoading1(false);
        // if (response.status == 200) {
        //     if (exit) {
        //         router.push('/categories');
        //     } else {
        //         setCategoryViName('');
        //         setCategoryEnName('');
        //         setDescriptionVi('');
        //         setDescriptionEn('');
        //         categoryViNameRef.current.focus();
        //     }
        // } else {
        //     alert('Có lỗi xảy ra');
        // }
    };

    const getCategories = async () => {
        try {
            const response = await apiClient.get(getCategoriesApi);
            // console.log(response.data);
            const data = response.data.data;
            const categories = data.map((item) => ({
                name: item.name,
                code: item.code
            }));
            setMultiselectValues(categories);

            // setLoading1(false);
        } catch (error) {
            console.error('error', error);
        }
    };

    useEffect(() => {
        getCategories();
    }, []);

    useEffect(() => {
        // getPermissions();
    }, []);

    const itemTemplate = (option) => {
        return (
            <div className="flex align-items-center">
                {/* <img
                    alt={option.name}
                    src={`/demo/images/flag/flag_placeholder.png`}
                    onError={(e) => (e.currentTarget.src = 'https://www.primefaces.org/wp-content/uploads/2020/05/placeholder.png')}
                    className={`flag flag-${option.code.toLowerCase()}`}
                    style={{ width: '21px' }}
                /> */}
                <span className="ml-2">{option.name}</span>
            </div>
        );
    };

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <div className="p-fluid formgrid grid">
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

                        <div className="field col-12">
                            <div className="grid mt-4 justify-content-end align-items-center gap-2">
                                {/* <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu và thêm mới" icon="pi pi-save" className="p-button-info" loading={loading1} onClick={() => saveRole()} />
                                </div> */}
                                <div className="col-12 md:col-6 lg:col-2">
                                    <Button label="Lưu" icon="pi pi-save" className="p-button-success" loading={loading1} onClick={() => saveRole(true)} />
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
