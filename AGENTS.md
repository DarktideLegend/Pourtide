# AGENTS.md

Guidance for coding agents working under `Source/`. This file applies to the C# solution and RealmProps definitions in this directory tree only.

## Project Context

- ACRealms is a .NET 9, x64 C# solution based on ACE/ACEmulator.
- The main solution file in this directory is `ACRealms.sln`.
- Realm property definitions live in `ACRealms.RealmProps/PropDefs/json/**/*.jsonc`.
- Generated RealmProps artifacts are derived from the JSONC property definitions and source generators.

## Agent Workflow

- Run `git status --short` before editing so existing user changes are visible.
- Preserve user changes and untracked files. Do not revert, delete, or overwrite unrelated work.
- Prefer `rg` and `rg --files` for searching.
- Keep edits narrowly scoped to the requested behavior.
- Follow nearby patterns before introducing new helpers, abstractions, or formatting styles.

## Response Format

Structure your entire response exactly like this and nothing else:

1. One-sentence summary.
2. Bullet points (max 3).
3. One clear next action.

Use markdown. No extra text.

## C# Conventions

- Match the style of the project and neighboring files.
- Respect each project file's settings, including disabled implicit usings and mixed nullable settings.
- Avoid broad refactors unless they are directly required for the requested change.
- Add focused tests for behavior changes, especially in shared server, database, RealmProps, or ruleset code.
- Do not manually edit generated source or build outputs unless explicitly requested.

## Comments

- Prefer useful comments for intent, gameplay rules, domain quirks, and non-obvious constraints.
- Preserve existing comments unless they become inaccurate.
- Avoid comments that only restate what the code already says.

## RealmProps Work

- For RealmProps definition changes, use the `generate-realm-props` skill for naming, schema shape, bounds, server-property mappings, and validation workflow.

## Verification Commands

- Full solution build: `dotnet build ACRealms.sln -p:Platform=x64`
- Main server tests: `dotnet test ACE.Server.Tests/ACRealms.Tests.csproj -p:Platform=x64`
- RealmProps-only build: `dotnet build ACRealms.RealmProps/ACRealms.RealmProps.csproj -p:Platform=x64`

