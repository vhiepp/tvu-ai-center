'use client';
import { Metadata } from 'next';
import Layout from '../../layout/layout';
import { useRouter } from 'next/navigation';
import useSWR from 'swr';
import { useEffect } from 'react';
import { profileApi } from '@/apis/auth-api';
import { fetcher } from '@/functions';

interface AppLayoutProps {
    children: React.ReactNode;
}

export const metadata: Metadata = {
    title: 'AI Center Admin',
    description: 'The ultimate collection of design-agnostic, flexible and accessible React UI Components.',
    robots: { index: false, follow: false },
    viewport: { initialScale: 1, width: 'device-width' },
    openGraph: {
        type: 'website',
        title: 'PrimeReact SAKAI-REACT',
        url: 'https://sakai.primereact.org/',
        description: 'The ultimate collection of design-agnostic, flexible and accessible React UI Components.',
        images: ['https://www.primefaces.org/static/social/sakai-react.png'],
        ttl: 604800
    },
    icons: {
        icon: '/favicon.ico'
    }
};

export default function AppLayout({ children }: AppLayoutProps) {
    const router = useRouter();
    const { data, error } = useSWR(profileApi, fetcher);

    useEffect(() => {
        if (error) {
            router.push('/auth/login');
        }
    }, [data, error]);

    return <Layout>{children}</Layout>;
}
