import { User } from './User'

export interface SessionContext {
  user?: User
  error?: Error
  loading: boolean
}
