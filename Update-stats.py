#!/usr/bin/env python3

from __future__ import annotations

import re
import subprocess
import sys
from collections import Counter
from datetime import date, datetime, timedelta
from pathlib import Path

try:
    from zoneinfo import ZoneInfo
except ImportError:  # very old Python
    ZoneInfo = None

TIMEZONE = "Asia/Dhaka"
WEEKS = 26  
CATEGORIES = {  
    "algorithms": "Algorithms",
    "sql": "SQL",
    "concurrency": "Concurrency & LLD",
    "builds": "Builds",
    "system-design": "System design",
}
BOT_PREFIX = "chore: update stats" 

ROOT = Path(__file__).resolve().parent.parent
README = ROOT / "README.md"
SVG_PATH = ROOT / "assets" / "progress.svg"
START_MARK = "<!-- STATS:START -->"
END_MARK = "<!-- STATS:END -->"
SKIP_DIRS = {".git", "bin", "obj", "node_modules", "__pycache__"}


def today() -> date:
    if ZoneInfo is not None:
        try:
            return datetime.now(ZoneInfo(TIMEZONE)).date()
        except Exception:
            pass
    return date.today()


def commit_counts() -> Counter:
    """Commits per day (author date), ignoring the stats bot's own commits."""
    result = subprocess.run(
        ["git", "log", "--date=short", "--format=%ad",
         "--invert-grep", f"--grep=^{BOT_PREFIX}"],
        cwd=ROOT, capture_output=True, text=True,
    )
    if result.returncode != 0:  # e.g. no commits yet
        return Counter()
    return Counter(date.fromisoformat(line) for line in result.stdout.split() if line)


def count_solutions() -> dict[str, int]:
    counts = {key: 0 for key in CATEGORIES}
    for key in CATEGORIES:
        base = ROOT / key
        if not base.is_dir():
            continue
        folders = set()
        for p in base.rglob("*"):
            if not p.is_file() or SKIP_DIRS & set(p.relative_to(ROOT).parts):
                continue
            if p.name.upper() == "NOTES.MD" or p.stem.lower() == "solution":
                folders.add(p.parent)
        counts[key] = len(folders)
    return counts


def streaks(days: set[date], now: date) -> tuple[int, int]:
    """Return (current, longest). A streak survives until the day ends."""
    current = 0
    d = now if now in days else now - timedelta(days=1)
    while d in days:
        current += 1
        d -= timedelta(days=1)

    longest = run = 0
    prev = None
    for d in sorted(days):
        run = run + 1 if prev is not None and d - prev == timedelta(days=1) else 1
        longest = max(longest, run)
        prev = d
    return current, longest


def level(n: int) -> int:
    return 0 if n == 0 else 1 if n == 1 else 2 if n == 2 else 3 if n <= 4 else 4


def build_svg(counts: Counter, now: date) -> str:
    cell, gap = 12, 3
    step = cell + gap
    left, top = 28, 20
    start = now - timedelta(days=now.weekday()) - timedelta(weeks=WEEKS - 1)
    width = left + WEEKS * step + 8
    height = top + 7 * step + 6

    parts = [
        f'<svg xmlns="http://www.w3.org/2000/svg" width="{width}" height="{height}" '
        f'viewBox="0 0 {width} {height}" role="img" '
        f'aria-label="Daily activity over the last {WEEKS} weeks">',
        "<style>"
        ".c0{fill:#ebedf0}.c1{fill:#9be9a8}.c2{fill:#40c463}.c3{fill:#30a14e}.c4{fill:#216e39}"
        "text{fill:#57606a;font:10px -apple-system,Segoe UI,Helvetica,Arial,sans-serif}"
        "@media (prefers-color-scheme: dark){"
        ".c0{fill:#161b22}.c1{fill:#0e4429}.c2{fill:#006d32}.c3{fill:#26a641}.c4{fill:#39d353}"
        "text{fill:#8b949e}}"
        "</style>",
    ]

    last_month = None
    for w in range(WEEKS):
        week_start = start + timedelta(weeks=w)
        x = left + w * step
        if week_start.month != last_month:
            crowded = w == 0 and (start + timedelta(weeks=1)).month != start.month
            if not crowded:
                parts.append(f'<text x="{x}" y="12">{week_start.strftime("%b")}</text>')
            last_month = week_start.month
        for d in range(7):
            day = week_start + timedelta(days=d)
            if day > now:
                continue
            n = counts.get(day, 0)
            plural = "" if n == 1 else "s"
            parts.append(
                f'<rect class="c{level(n)}" x="{x}" y="{top + d * step}" '
                f'width="{cell}" height="{cell}" rx="2">'
                f"<title>{day.isoformat()}: {n} commit{plural}</title></rect>"
            )

    for d, label in ((0, "Mon"), (2, "Wed"), (4, "Fri")):
        parts.append(f'<text x="0" y="{top + d * step + 10}">{label}</text>')
    parts.append("</svg>")
    return "\n".join(parts)


def affirmation(current: int, done_today: bool, active_days: int) -> str:
    if active_days == 0:
        return "Day one is the hardest. Commit your first solution today."
    if current == 0:
        return "The streak reset, the skills didn't. Solve one today and start a new run."
    if not done_today:
        return f"{current}-day streak is on the line. One solution keeps it alive."
    if current < 7:
        return f"{current} in a row. Consistency is built exactly like this."
    if current < 30:
        return f"{current} days straight. You are becoming the engineer who shows up."
    return f"{current} days straight. This is no longer motivation, it's who you are."


def bar(n: int, biggest: int, width: int = 10) -> str:
    filled = round(width * n / biggest) if biggest else 0
    return "█" * filled + "░" * (width - filled)


def build_block(sol: dict[str, int], counts: Counter, now: date) -> str:
    days = set(counts)
    current, longest = streaks(days, now)
    total = sum(sol.values())
    biggest = max(sol.values()) if sol else 0

    rows = "\n".join(
        f"| {label} | {sol[key]} | `{bar(sol[key], biggest)}` |"
        for key, label in CATEGORIES.items()
    )
    return (
        f'<p align="center"><img src="assets/progress.svg" '
        f'alt="Activity over the last {WEEKS} weeks"></p>\n\n'
        f"> **{affirmation(current, now in days, len(days))}**\n\n"
        "| Current streak | Longest streak | Solutions | Active days |\n"
        "|:-:|:-:|:-:|:-:|\n"
        f"| **{current}** days | **{longest}** days | **{total}** | **{len(days)}** |\n\n"
        "| Category | Solved | |\n"
        "|---|--:|---|\n"
        f"{rows}\n\n"
        f"<sub>Last updated {now.isoformat()} ({TIMEZONE}). Generated by "
        "<code>scripts/update_stats.py</code>.</sub>"
    )


def update_readme(block: str) -> bool:
    text = README.read_text(encoding="utf-8")
    pattern = re.compile(re.escape(START_MARK) + r".*?" + re.escape(END_MARK), re.S)
    if not pattern.search(text):
        sys.exit(f"Could not find {START_MARK} ... {END_MARK} in README.md")
    new_text = pattern.sub(lambda _: f"{START_MARK}\n{block}\n{END_MARK}", text)
    if new_text == text:
        return False
    README.write_text(new_text, encoding="utf-8")
    return True


def main() -> None:
    now = today()
    counts = commit_counts()
    block = build_block(count_solutions(), counts, now)

    SVG_PATH.parent.mkdir(parents=True, exist_ok=True)
    SVG_PATH.write_text(build_svg(counts, now), encoding="utf-8")
    changed = update_readme(block)
    print("README updated." if changed else "README already up to date.")


if __name__ == "__main__":
    main()