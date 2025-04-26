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
import { Calendar } from 'primereact/calendar';
import { createMemberApi } from '@/apis/member';

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

    const [avatar, setAvatar] = useState<any>(null);
    const [fullName, setFullName] = useState('');
    const [position, setPosition] = useState('');
    const [skills, setSkills] = useState('');
    const [calendarValue, setCalendarValue] = useState<any>(new Date());
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [description, setDescription] = useState(null);
    const [githubLink, setGithubLink] = useState('');
    const [facebookLink, setFacebookLink] = useState('');
    const [instagramLink, setInstagramLink] = useState('');
    const [linkedlnLink, setLinkedlnLink] = useState('');
    const [switchValue, setSwitchValue] = useState(false);

    const fullNameRef = useRef(null);
    const positionRef = useRef(null);
    const skillsRef = useRef(null);
    const dateRef = useRef(null);

    const router = useRouter();

    // const getPermissions = async () => {
    //     const response = await apiClient.get(getAdminPermissionsApi);
    //     const pers = response.data.data;
    //     pers.forEach((per) => {
    //         setSwitchValue((prev) => ({ ...prev, [per.id]: false }));
    //     });
    //     setPermissions(pers);
    // };

    const handleSaveContent = (content: any, language: string) => {
        setDescription(content);
    };

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
        if (!position) {
            positionRef.current.focus();
            return;
        }
        if (!skills) {
            skillsRef.current.focus();
            return;
        }
        if (!calendarValue) {
            dateRef.current.focus();
            return;
        }
        if (!avatar) {
            alert('Vui lòng chọn ảnh đại diện');
            return;
        }
        setLoading1(true);
        const formData = new FormData();
        const file = base64ToFile(avatar, 'upload.png');
        formData.append('Avatar', file);
        formData.append('Fullname', fullName);
        formData.append('Position', position);
        formData.append('Skills', skills);
        formData.append('DateOfJoin', calendarValue.toISOString().split('T')[0]);
        // console.log('calendarValue', calendarValue.toISOString().split('T')[0]);
        formData.append('Email', email);
        formData.append('PhoneNumber', phone);
        formData.append('Description', JSON.stringify(description));
        formData.append('Github', githubLink);
        formData.append('Facebook', facebookLink);
        formData.append('Instagram', instagramLink);
        formData.append('Linkedln', linkedlnLink);
        // @ts-ignore
        formData.append('Pin', switchValue);

        const response = await apiClient.post(createMemberApi, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });

        setLoading1(false);
        if (response.status == 200) {
            if (exit) {
                router.push('/members');
            } else {
                setAvatar(null);
                setFullName('');
                setPosition('');
                setSkills('');
                setCalendarValue(new Date());
                setEmail('');
                setPhone('');
                setDescription(null);
                setGithubLink('');
                setFacebookLink('');
                setInstagramLink('');
                setLinkedlnLink('');
                setSwitchValue(false);
            }
        } else {
            alert('Có lỗi xảy ra');
        }
    };

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <h5>Thêm thành viên</h5>
                    <div className="p-fluid formgrid grid">
                        <div className="field col-12 md:col-6">
                            <label htmlFor="password" className="text-lg">
                                Ảnh đại diện <span className="p-error">(*)</span>
                            </label>
                            <ImagePicker setImageSrc={setAvatar} width="250px" height="250px" objectFit="cover" />
                        </div>

                        <div className="field col-12 md:col-6">
                            <div className="grid">
                                <div className="field col-12">
                                    <label htmlFor="role_name" className="text-lg">
                                        Họ tên <span className="p-error">(*)</span>
                                    </label>
                                    <InputText ref={fullNameRef} placeholder="Họ tên đầy đủ" value={fullName} onChange={(e) => setFullName(e.target.value)} type="text" />
                                </div>
                                <div className="field col-12">
                                    <label htmlFor="role_name" className="text-lg">
                                        Chức vụ <span className="p-error">(*)</span>
                                    </label>
                                    <InputText ref={positionRef} placeholder="Giám đốc, phó giám đốc, nhân viên,..." value={position} onChange={(e) => setPosition(e.target.value)} type="text" />
                                </div>
                                <div className="field col-12">
                                    <label htmlFor="role_name" className="text-lg">
                                        Chuyên môn <span className="p-error">(*)</span>
                                    </label>
                                    <InputText ref={skillsRef} placeholder="NLP, Machine learning,..." value={skills} onChange={(e) => setSkills(e.target.value)} type="text" />
                                </div>
                                <div className="field col-12">
                                    <label htmlFor="password" className="text-lg">
                                        Ngày gia nhập <span className="p-error">(*)</span>
                                    </label>
                                    <Calendar ref={dateRef} showIcon showButtonBar dateFormat="dd/mm/yy" maxDate={new Date()} value={calendarValue} onChange={(e) => setCalendarValue(e.value ?? null)} />
                                </div>
                            </div>
                        </div>

                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Email
                            </label>
                            <InputText placeholder="Nhập email (nếu có)" value={email} onChange={(e) => setEmail(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Số điện thoại
                            </label>
                            <InputText placeholder="Số điện thoại (nếu có)" value={phone} onChange={(e) => setPhone(e.target.value)} type="text" />
                        </div>

                        <div className="field col-12">
                            <label htmlFor="address" className="text-lg">
                                Mô tả bản thân{' '}
                            </label>
                            <Editor onChange={handleSaveContent} data={description} />
                        </div>

                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Link Github
                            </label>
                            <InputText placeholder="https://..." value={githubLink} onChange={(e) => setGithubLink(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Link Facebook
                            </label>
                            <InputText placeholder="https://..." value={facebookLink} onChange={(e) => setFacebookLink(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Link Instagram
                            </label>
                            <InputText placeholder="https://..." value={instagramLink} onChange={(e) => setInstagramLink(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <label htmlFor="role_name" className="text-lg">
                                Link Linkedln
                            </label>
                            <InputText placeholder="https://..." value={linkedlnLink} onChange={(e) => setLinkedlnLink(e.target.value)} type="text" />
                        </div>
                        <div className="field col-12 md:col-6">
                            <div className="flex align-items-center">
                                <label htmlFor="role_name" className="text-lg mr-2">
                                    Ghim vào trang giới thiệu:
                                </label>
                                <InputSwitch checked={switchValue} onChange={(e) => setSwitchValue(e.value ?? false)} />
                            </div>
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
