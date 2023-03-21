import { createContext, ReactNode, useMemo, useState } from 'react';
import { SessionContext } from '../models/SessionContext';
import { User } from '../models/User';

export const AuthContext = createContext<SessionContext | null>(
  {} as SessionContext
);

export const AuthProvider = (children: ReactNode) => {
  const [user, setUser] = useState<User>();
  const [error, setError] = useState<Error>();
  const [loading, setLoading] = useState<boolean>(false);
  const context = useMemo(
    () => ({
      user,
      loading,
      error,
    }),
    [user, loading, error]
  );

  return (
    <AuthContext.Provider value={context}>
      {!loading && children}
    </AuthContext.Provider>
  );
};
