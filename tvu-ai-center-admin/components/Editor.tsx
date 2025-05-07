'use client';

import { useEffect, useRef, useState } from 'react';
import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header';
import List from '@editorjs/list';
import Quote from '@editorjs/quote';
import Marker from '@editorjs/marker';
import CodeTool from '@editorjs/code';
import Delimiter from '@editorjs/delimiter';
import Table from '@editorjs/table';
import ImageTool from '@editorjs/image';
import '@/public/css/editor.css';

export default function Editor({ onChange, data, readOnly = false }) {
    const ejInstance = useRef(null);
    const [editorId, setEditorId] = useState('');

    useEffect(() => {
        const randomId = `editorjs-${Math.random()}`;
        setEditorId(randomId);
    }, []);

    useEffect(() => {
        if (!ejInstance.current && editorId !== '') {
            const editor = new EditorJS({
                // placeholder: 'Soạn nội dung tiếng việt...',
                holder: editorId,
                inlineToolbar: true,
                autofocus: false,
                readOnly,
                data: data || {},
                tools: {
                    header: Header,
                    list: List,
                    quote: Quote,
                    // warning: Warning,
                    marker: Marker,
                    code: CodeTool,
                    delimiter: Delimiter,
                    table: {
                        // @ts-ignore
                        class: Table,
                        inlineToolbar: true,
                        config: {
                            rows: 2,
                            cols: 3,
                            withHeadings: true,
                            withDropdown: true
                        }
                    },
                    image: {
                        class: ImageTool,
                        config: {
                            /**
                             * Đường dẫn API xử lý upload ảnh
                             */
                            endpoints: {
                                byFile: 'http://localhost:3020/save-image-editorjs' // Upload ảnh từ file
                            }
                        }
                    }
                },
                onReady: () => {
                    ejInstance.current = editor;
                },
                onChange: async () => {
                    const content = await editor.save();
                    // console.log('Content changed:', content);
                    if (onChange) onChange(content);
                }
            });
        }

        return () => {
            if (ejInstance.current && ejInstance.current.destroy) {
                ejInstance.current.destroy();
                ejInstance.current = null;
            }
        };
    }, [editorId, readOnly]);

    return (
        <div
            id={editorId}
            style={{
                border: '1px solid #ccc',
                borderRadius: '4px',
                padding: '10px'
            }}
        />
    );
}
