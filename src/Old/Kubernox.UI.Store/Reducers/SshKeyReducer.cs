using Fluxor;
using Kubernox.UI.Store.Actions.SshKey;
using Kubernox.UI.Store.States;
using System.Linq;

namespace Kubernox.UI.Store.Reducers
{
    public static class SshKeyReducer
    {
        [ReducerMethod]
        public static SshKeyState ReduceFetchSshKeyAction(SshKeyState state, FetchSshKeyAction _) =>
           new SshKeyState(null, true, null);

        [ReducerMethod]
        public static SshKeyState ReduceFetchSshKeySuccessAction(SshKeyState state, FetchSshKeySuccessAction action) =>
            new SshKeyState(action.Keys.ToArray(), false, null);

        [ReducerMethod]
        public static SshKeyState ReduceFetchSshKeyFailureAction(SshKeyState state, FetchSshKeyFailureAction action) =>
            new SshKeyState(null, false, action.ErrorMessage);

        [ReducerMethod]
        public static SshKeyState ReduceCreateSshKeyAction(SshKeyState state, CreateSshKeyAction _) =>
          new SshKeyState(state.SshKeys, true, null);

        [ReducerMethod]
        public static SshKeyState ReduceCreateSshKeySuccessAction(SshKeyState state, CreateSshKeySuccessAction action) =>
            new SshKeyState(state.SshKeys, false, null);

        [ReducerMethod]
        public static SshKeyState ReduceCreateSshKeyFailureAction(SshKeyState state, CreateSshKeyFailureAction action) =>
            new SshKeyState(null, false, action.ErrorMessage);

        [ReducerMethod]
        public static SshKeyState ReduceDeleteSshKeyAction(SshKeyState state, DeleteSshKeyAction _) =>
          new SshKeyState(state.SshKeys, true, null);

        [ReducerMethod]
        public static SshKeyState ReduceDeleteSshKeySuccessAction(SshKeyState state, DeleteSshKeySuccessAction action) =>
            new SshKeyState(state.SshKeys, false, null);

        [ReducerMethod]
        public static SshKeyState ReduceDeleteSshKeyFailureAction(SshKeyState state, DeleteSshKeyFailureAction action) =>
            new SshKeyState(state.SshKeys, false, action.ErrorMessage);
    }
}

