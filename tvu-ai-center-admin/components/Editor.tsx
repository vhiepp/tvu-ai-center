'use client';

import { use, useEffect, useRef, useState } from 'react';
import EditorJS from '@editorjs/editorjs';
import Header from '@editorjs/header';
import List from '@editorjs/list';

export default function Editor({ onChange, data }) {
    const ejInstance = useRef(null);
    const [editorId, setEditorId] = useState('');

    useEffect(() => {
        const randomId = `editorjs-${Math.random()}`;
        setEditorId(randomId);
    }, []);

    useEffect(() => {
        if (!ejInstance.current && editorId !== '') {
            const editor = new EditorJS({
                holder: editorId,
                autofocus: true,
                data: data || {},
                tools: {
                    header: Header,
                    list: List
                },
                onReady: () => {
                    ejInstance.current = editor;
                },
                onChange: async () => {
                    const content = await editor.save();
                    console.log('Content changed:', content);
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
    }, [editorId]);

    return (
        <div
            id={editorId}
            style={{
                border: '1px solid #ccc',
                borderRadius: '4px'
            }}
        />
    );
}
